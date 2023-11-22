using RestSharp;
using System.Net;
using System.Security;
using System.Text.Json;
using System.Windows.Forms;
using AppCuesTranslations.AppCuesFiles;
using static AppCuesTranslations.LanguageSettings;
using static AppCuesTranslations.AppCuesFiles.AppCuesFileSV;
using static System.Windows.Forms.Design.AxImporter;

namespace AppCuesTranslations
{
    public partial class Form1 : Form
    {
        private LanguageSettings.Root _settings = new LanguageSettings.Root();
        private AppCuesFileDA.Root _appCuesFileDA = new AppCuesFileDA.Root();
        private AppCuesFileDE.Root _appCuesFileDE = new AppCuesFileDE.Root();
        private AppCuesFileFI.Root _appCuesFileFI = new AppCuesFileFI.Root();
        private AppCuesFileNB.Root _appCuesFileNB = new AppCuesFileNB.Root();
        private AppCuesFileSV.Root _appCuesFileSV = new AppCuesFileSV.Root();

        private TranslationKeyResult _translationsResult = new TranslationKeyResult();
        private RestClient _lokaliseClient;

        private List<string> _missingInEnglish = new List<string>();
        private Dictionary<string, List<string>> _translationMissing = new Dictionary<string, List<string>>();

        public Form1()
        {
            InitializeComponent();
        }

        private void SelectFolderButton_Click(object sender, EventArgs e)
        {
            GetSettings();

            if (_settings.LokaliseToken == null || _settings.LokaliseProjectId == null)
            {
                FeedbackLabel.Text = "API token or Lokalise project ID missing. Settings file not found?";
                FeedbackLabel.BackColor = System.Drawing.Color.LightPink;
                return;
            }

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] files = Directory.GetFiles(folderBrowserDialog.SelectedPath, "*.json");

                    foreach (string file in files)
                    {
                        filesListBox.Items.Add(file.Substring(folderBrowserDialog.SelectedPath.Length + 1));
                        filesListBox.SetSelected(filesListBox.Items.Count - 1, true);
                    }

                    addTranslationsButton.Enabled = true;

                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }

            FolderLabel.Text = $"Folder: {folderBrowserDialog.SelectedPath}";
            FolderLabel.Visible = filesListBox.Visible = overwriteCheckBox.Visible = addTranslationsButton.Visible = true;
        }

        private void GetSettings()
        {
            using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + "/settings.json"))
            {
                _settings = JsonSerializer.Deserialize<LanguageSettings.Root>(r.BaseStream);
            }
        }

        private void buttonGetTranslations_Click(object sender, EventArgs e)
        {
            CreateLokaliseClient();
            GetTranslationsFromLokalise();

            var folderPath = folderBrowserDialog.SelectedPath;

            foreach (var selectedFile in filesListBox.SelectedItems)
            {
                var filePath = $"{folderPath}\\{selectedFile}";
                var language = _settings.Languages.Single(l => selectedFile.ToString().ToLower().Contains(l.Name.ToLower()));

                ProcessFileContent(filePath, language);
                SaveFile(filePath, language);
            }

            ErrorFeedback();
        }

        private void CreateLokaliseClient()
        {
            var options = new RestClientOptions("https://api.lokalise.com")
            {
                MaxTimeout = -1,
            };
            _lokaliseClient = new RestClient(options);
        }

        private void GetTranslationsFromLokalise(int page = 1, int limit = 1000)
        {
            logLabel.Text += $"Fetching translations {page*limit-limit+1} to {page*limit} from Lokalise.\n";
            var request = new RestRequest($"/api2/projects/{_settings.LokaliseProjectId}/translations?page={page}&limit={limit}", Method.Get);
            request.AddHeader("X-Api-Token", _settings.LokaliseToken);
            RestResponse response = _lokaliseClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                logLabel.Text += "Translations fetched.\n";

                var result = JsonSerializer.Deserialize<TranslationKeyResult>(response.Content);

                if (_translationsResult.Translations == null)
                    _translationsResult.Translations = new List<TranslationKeyResult.Translation>();

                _translationsResult.Translations.AddRange(result.Translations);

                int totalPageCount = Convert.ToInt32(response.Headers.ToList().Find(x => x.Name == "X-Pagination-Page-Count").Value);
                int totalTranslationCount = Convert.ToInt32(response.Headers.ToList().Find(x => x.Name == "X-Pagination-Total-Count").Value);

                if (page < totalPageCount)
                    GetTranslationsFromLokalise(page: page + 1);

                if (totalTranslationCount != _translationsResult.Translations.Count())
                {
                    FeedbackLabel.Text = $"Number of translations retrieved does not match the expected number.";
                    FeedbackLabel.BackColor = System.Drawing.Color.LightPink;
                    return;
                }

            }
            else
            {
                FeedbackLabel.Text = $"Error calling Lokalise: {response.StatusCode.ToString()}";
                FeedbackLabel.BackColor = System.Drawing.Color.LightPink;
                return;
            }
        }

        private void ProcessFileContent(string filePath, Language language)
        {
            if (language.IsoCode == "da")
            {
                logLabel.Text += "Processing Danish file.\n";
                using (StreamReader r = new StreamReader(filePath))
                {
                    _appCuesFileDA = JsonSerializer.Deserialize<AppCuesFileDA.Root>(r.BaseStream);
                }

                if (_appCuesFileDA != null)
                {
                    foreach (var content in _appCuesFileDA.Content)
                    {
                        string translation = GetTranslation(language, content.Default);
                        if (!String.IsNullOrEmpty(translation) || overwriteCheckBox.Checked)
                            content.Danish = translation;
                    }
                }
                else
                    logLabel.Text += "Unable to process Danish file.\n";
            }
            else if (language.IsoCode == "de")
            {
                logLabel.Text += "Processing German file.\n";
                using (StreamReader r = new StreamReader(filePath))
                {
                    _appCuesFileDE = JsonSerializer.Deserialize<AppCuesFileDE.Root>(r.BaseStream);
                }

                if (_appCuesFileDE != null)
                {
                    foreach (var content in _appCuesFileDE.Content)
                    {
                        string translation = GetTranslation(language, content.Default);
                        if (!String.IsNullOrEmpty(translation) || overwriteCheckBox.Checked)
                            content.German = translation;
                    }
                }
                else
                    logLabel.Text += "Unable to process German file.\n";
            }
            else if (language.IsoCode == "fi")
            {
                logLabel.Text += "Processing Finnish file.\n";
                using (StreamReader r = new StreamReader(filePath))
                {
                    _appCuesFileFI = JsonSerializer.Deserialize<AppCuesFileFI.Root>(r.BaseStream);
                }

                if (_appCuesFileFI != null)
                {
                    foreach (var content in _appCuesFileFI.Content)
                    {
                        string translation = GetTranslation(language, content.Default);
                        if (!String.IsNullOrEmpty(translation) || overwriteCheckBox.Checked)
                            content.Finnish = translation;
                    }
                }
                else
                    logLabel.Text += "Unable to process Finnish file.\n";
            }
            else if (language.IsoCode == "nb")
            {
                logLabel.Text += "Processing Norwegian file.\n";
                using (StreamReader r = new StreamReader(filePath))
                {
                    _appCuesFileNB = JsonSerializer.Deserialize<AppCuesFileNB.Root>(r.BaseStream);
                }

                if (_appCuesFileNB != null)
                {
                    foreach (var content in _appCuesFileNB.Content)
                    {
                        string translation = GetTranslation(language, content.Default);
                        if (!String.IsNullOrEmpty(translation) || overwriteCheckBox.Checked)
                            content.Norwegian = translation;
                    }
                }
                else
                    logLabel.Text += "Unable to process Norwegian file.\n";
            }
            else if (language.IsoCode == "sv")
            {
                logLabel.Text += "Processing Swedish file.\n";
                using (StreamReader r = new StreamReader(filePath))
                {
                    _appCuesFileSV = JsonSerializer.Deserialize<AppCuesFileSV.Root>(r.BaseStream);
                }

                if (_appCuesFileSV != null)
                {
                    foreach (var content in _appCuesFileSV.Content)
                    {
                        string translation = GetTranslation(language, content.Default);
                        if (!String.IsNullOrEmpty(translation) || overwriteCheckBox.Checked)
                            content.Swedish = translation;
                    }
                }
                else
                    logLabel.Text += "Unable to process Swedish file.\n";
            }
        }

        private string GetTranslation(Language language, string englishContent)
        {
            var english = _translationsResult.Translations.SingleOrDefault(t => t.Value == englishContent);
            if (english == null)
            {
                if (!_missingInEnglish.Contains(englishContent))
                    _missingInEnglish.Add(englishContent);
                return "";
            }
            var translation = _translationsResult.Translations.SingleOrDefault(t => t.Key == english.Key && t.LanguageIso == language.IsoCode);
            if (translation == null)
            {
                if (_translationMissing.ContainsKey(language.Name))
                    _translationMissing[language.Name].Add(englishContent);
                else
                    _translationMissing.Add(language.Name, new List<string> { englishContent });
                return "";
            }
            return translation.Value;
        }

        private void SaveFile(string filePath, Language language)
        {
            switch (language.IsoCode)
            {
                case "da":
                    {
                        JsonFileUtils.PrettyWrite(_appCuesFileDA, filePath);
                        logLabel.Text += "Danish file updated!\n";
                        break;
                    }
                case "de":
                    {
                        JsonFileUtils.PrettyWrite(_appCuesFileDE, filePath);
                        logLabel.Text += "German file updated!\n";
                        break;
                    }
                case "fi":
                    {
                        JsonFileUtils.PrettyWrite(_appCuesFileFI, filePath);
                        logLabel.Text += "Finnish file updated!\n";
                        break;
                    }
                case "nb":
                    {
                        JsonFileUtils.PrettyWrite(_appCuesFileNB, filePath);
                        logLabel.Text += "Norwegian file updated!\n";
                        break;
                    }
                case "sv":
                    {
                        JsonFileUtils.PrettyWrite(_appCuesFileSV, filePath);
                        logLabel.Text += "Swedish file updated!\n";
                        break;
                    }
            }
        }

        private void ErrorFeedback()
        {
            if (_missingInEnglish.Count > 0)
                logLabel.Text += $"Could not find key(s) with English text:\n";

            foreach (var missingText in _missingInEnglish)
            {
                logLabel.Text += $"  -    {missingText}\n";
            }
            foreach (var lang in _translationMissing)
            {
                foreach (var text in lang.Value)
                {
                    logLabel.Text += $"Could not find translation in {lang} for: {text}\n";
                }
            }
        }
    }
}
