using System.Diagnostics;
using System.Reflection;

namespace JpegFileUpdater;

public partial class MainForm : Form
{
    private IEnumerable<FileInfo> JpegFiles =>
        FileListTextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(x => new FileInfo(x));

    private bool IsLoading { get; set; } = true;

    private string LastBackupDir { get; set; } = string.Empty;

    public MainForm(string[]? args)
    {
        InitializeComponent();
        Text = $"{APP_FULL_NAME} - {FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion}";
        MaximumSize = Size;
        MinimumSize = Size;
        MaximizeBox = false;
        var appConfig = AppConfig.Load();
        ExifToolPathTextBox.Text = appConfig.ExifToolPath;
        BaseFileNameTextBox.Text = appConfig.BaseFileName;
        DigitTextBox.Text = appConfig.Digit.ToString();
        StartNumberTextBox.Text = appConfig.StartNumber.ToString();
        BaseDateTimePicker.Value = appConfig.BaseDateTime;
        SecondsTextBox.Text = appConfig.Seconds.ToString();
        AppendToFileListTextBox(args);
        ShowStatusMessage();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        IsLoading = false;
    }

    private void MainForm_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
        {
            e.Effect = DragDropEffects.All;
        }
        else
        {
            e.Effect = DragDropEffects.None;
        }
    }

    private void MainForm_DragDrop(object sender, DragEventArgs e)
    {
        var files = e.Data?.GetData(DataFormats.FileDrop) as string[];
        AppendToFileListTextBox(files);
    }

    private void AppendToFileListTextBox(string[]? files)
    {
        if (files == null || files.Length == 0)
        {
            return;
        }
        FileListTextBox.AppendText(string.Join(Environment.NewLine, GetJpegFileListRecursive(files)));
        FileListTextBox.AppendText(Environment.NewLine);
        FileListTextBox.Focus();
    }

    private IEnumerable<string> GetJpegFileListRecursive(string[] files)
    {
        var result = new List<string>();
        foreach (var file in files)
        {
            if (Directory.Exists(file))
            {
                result.AddRange(GetJpegFileListRecursive(Directory.GetFiles(file, "*", SearchOption.AllDirectories)));
            }
            else if (File.Exists(file) && Path.GetExtension(file).ToLower().IsIn(".jpg", ".jpeg"))
            {
                result.Add(file);
            }
        }
        return result;
    }

    private void FileListTextBox_TextChanged(object sender, EventArgs e)
    {
        ShowStatusMessage();
    }

    private void ShowStatusMessage()
    {
        StatusLabel.Text = string.Empty;
        SortButton.Enabled = false;
        RenameButton.Enabled = false;
        UpdateButton.Enabled = false;
        if (!JpegFiles.Any())
        {
            StatusLabel.Text = "JPEGファイル、またはJPEGファイルが入ったフォルダーをフォームにドラッグ＆ドロップしてください。";
            return;
        }
        StatusLabel.Text = $"ファイル数: {JpegFiles.Count()}";
        SortButton.Enabled = true;
        RenameButton.Enabled = true;
        UpdateButton.Enabled = true;
    }

    private void ClearButton_Click(object sender, EventArgs e)
    {
        FileListTextBox.Clear();
        FileListTextBox.Focus();
    }

    private void SortButton_Click(object sender, EventArgs e)
    {
        var sorted = JpegFiles
            .Where(x => x.Extension.ToLower().IsIn(".jpg", ".jpeg"))
            .Where(x => x.Exists)
            .Select(x => x.FullName)
            .Distinct()
            .OrderBy(x => x)
            .ToArray();
        FileListTextBox.Text = string.Join(Environment.NewLine, sorted);
        FileListTextBox.AppendText(Environment.NewLine);
        FileListTextBox.Focus();
    }

    private void UpdateButton_Click(object sender, EventArgs e)
    {
        if (!File.Exists(ExifToolPathTextBox.Text))
        {
            ShowWarningMessage("ExifTool が見つかりません。");
            return;
        }
        if (!CheckFileList())
        {
            return;
        }
        if (MessageBox.Show("日付を変更します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }
        BackupOriginalFiles();
        try
        {
            var newDateTime = BaseDateTimePicker.Value;
            foreach (var file in JpegFiles)
            {
                // ExifTool で撮影日時変更
                var arguments = $"-overwrite_original -alldates=\"{newDateTime:yyyy-MM-dd HH:mm:ss}\" \"{file.FullName}\"";
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = ExifToolPathTextBox.Text,
                        Arguments = arguments,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                    },
                };
                process.Start();
                StatusLabel.Text = $"ExifTool で処理中... {file.Name}";
                process.WaitForExit();
                if (process.ExitCode != 0)
                {
                    throw new Exception($"ExifTool でエラーが発生しました。{Environment.NewLine}{process.StandardOutput.ReadToEnd()}");
                }
                // ファイルの更新日時変更
                file.LastWriteTime = newDateTime;
                // 指定秒数をインクリメント
                newDateTime = newDateTime.AddSeconds(SecondsTextBox.Text.ToInt(1));
            }
            MessageBox.Show(
                new StringBuilder()
                    .AppendLine("日付変更が完了しました。")
                    .AppendLine()
                    .AppendLine("処理前のファイルは以下にバックアップされています。")
                    .AppendLine(LastBackupDir)
                    .ToString(),
                "日時更新完了",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            FileListTextBox.Clear();
            FileListTextBox.Focus();
        }
        catch (Exception ex)
        {
            StatusLabel.Text = string.Empty;
            ShowWarningMessage(new StringBuilder()
                .AppendLine("エラーが発生したため処理を中断しました。")
                .AppendLine()
                .AppendLine(ex.ToString())
                .AppendLine()
                .AppendLine("処理前のファイルは以下にバックアップされています。")
                .AppendLine(LastBackupDir)
                .ToString());
        }
    }

    private void RenameButton_Click(object sender, EventArgs e)
    {
        if (!CheckFileList())
        {
            return;
        }
        if (MessageBox.Show("リネームします。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }
        BackupOriginalFiles();
        try
        {
            var baseFileName = BaseFileNameTextBox.Text.Trim();
            var number = StartNumberTextBox.Text.ToInt(1);
            var digit = DigitTextBox.Text.ToInt(3);
            List<(FileInfo file, string newFileName)> renameList
                = JpegFiles.Select(x => (x, $"{baseFileName}_{number++.ToString($"D{digit}")}")).ToList();
            // 衝突防止のため、いったんGUID付きのファイル名にリネーム
            renameList.AsParallel().ForAll(x =>
            {
                x.file.MoveTo(Path.Combine(x.file.DirectoryName!, $"{x.newFileName}_{Guid.NewGuid()}{x.file.Extension}"));
            });
            // 指定されたファイル名にリネーム
            renameList.AsParallel().ForAll(x =>
            {
                x.file.MoveTo(Path.Combine(x.file.DirectoryName!, $"{x.newFileName}{x.file.Extension}"));
            });
            MessageBox.Show(
                new StringBuilder()
                    .AppendLine("リネームが完了しました。")
                    .AppendLine()
                    .AppendLine("処理前のファイルは以下にバックアップされています。")
                    .AppendLine(LastBackupDir)
                    .ToString(),
                "リネーム完了",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            FileListTextBox.Clear();
            FileListTextBox.Focus();
        }
        catch (Exception ex)
        {
            StatusLabel.Text = string.Empty;
            ShowWarningMessage(new StringBuilder()
                .AppendLine("エラーが発生したため処理を中断しました。")
                .AppendLine()
                .AppendLine(ex.ToString())
                .AppendLine()
                .AppendLine("処理前のファイルは以下にバックアップされています。")
                .AppendLine(LastBackupDir)
                .ToString());
        }
    }

    private static void ShowWarningMessage(string message)
        => MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);

    private bool CheckFileList()
    {
        var invalidExts = JpegFiles.Where(x => !x.Extension.ToLower().IsIn(".jpg", ".jpeg")).ToArray();
        if (invalidExts.Length != 0)
        {
            ShowWarningMessage($"JPEGファイルではないファイルがあります。{Environment.NewLine}{string.Join(Environment.NewLine, invalidExts.Select(x => x.FullName))}");
            return false;
        }
        var duplicates = JpegFiles.GroupBy(x => x.FullName).Where(x => x.Count() > 1).ToArray();
        if (duplicates.Length != 0)
        {
            ShowWarningMessage($"重複しているファイルがあります。{Environment.NewLine}{string.Join(Environment.NewLine, duplicates.Select(x => x.Key))}");
            return false;
        }
        var notExists = JpegFiles.Where(x => !x.Exists).ToArray();
        if (notExists.Length != 0)
        {
            ShowWarningMessage($"存在しないファイルがあります。{Environment.NewLine}{string.Join(Environment.NewLine, notExists.Select(x => x.FullName))}");
            return false;
        }
        return true;
    }

    private void BackupOriginalFiles()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), $"{APP_NAME}_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid()}");
        Directory.CreateDirectory(tempDir);
        foreach (var file in JpegFiles)
        {
            var dest = Path.Combine(tempDir, file.Name);
            file.CopyTo(dest);
        }
        LastBackupDir = tempDir;
    }

    private void ExifToolPathTextBox_TextChanged(object sender, EventArgs e)
    {
        if (IsLoading)
        {
            return;
        }
        AppConfig.SaveExifToolPath(ExifToolPathTextBox.Text);
    }

    private void BaseFileNameTextBox_TextChanged(object sender, EventArgs e)
    {
        if (IsLoading)
        {
            return;
        }
        AppConfig.SaveBaseFileName(BaseFileNameTextBox.Text);
    }

    private void DigitTextBox_TextChanged(object sender, EventArgs e)
    {
        if (IsLoading)
        {
            return;
        }
        var digit = DigitTextBox.Text.ToInt(3);
        if (DigitTextBox.Text != digit.ToString())
        {
            DigitTextBox.Text = digit.ToString();
        }
        AppConfig.SaveDigit(digit);
    }

    private void StartNumberTextBox_TextChanged(object sender, EventArgs e)
    {
        if (IsLoading)
        {
            return;
        }
        var number = StartNumberTextBox.Text.ToInt(1);
        if (StartNumberTextBox.Text != number.ToString())
        {
            StartNumberTextBox.Text = number.ToString();
        }
        AppConfig.SaveStartNumber(number);
    }

    private void BaseDateTimePicker_ValueChanged(object sender, EventArgs e)
    {
        if (IsLoading)
        {
            return;
        }
        AppConfig.SaveBaseDateTime(BaseDateTimePicker.Value);
    }

    private void SecondsTextBox_TextChanged(object sender, EventArgs e)
    {
        if (IsLoading)
        {
            return;
        }
        var seconds = SecondsTextBox.Text.ToInt(1);
        if (SecondsTextBox.Text != seconds.ToString())
        {
            SecondsTextBox.Text = seconds.ToString();
        }
        AppConfig.SaveSeconds(seconds);
    }
}
