using System.Drawing.Imaging;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using MetadataExtractor;
using Directory = System.IO.Directory;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.QuickTime;
using MetadataExtractor.Formats.Png;
using System.ComponentModel.DataAnnotations;

namespace GooglePhotosTakeoutOrganize
{
    public partial class FrmMain : Form
    {
        List<Tuple<String, String>> directories = new List<Tuple<String, String>>();
        bool validated = false;
        bool scanned = false;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tbxFolderPath.Text = folderBrowserDialog1.SelectedPath;
                validated = false;
                scanned = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "...";
            directories.Clear();
            tbxLog.Clear();
            tbxLog.AppendText("Validate" + Environment.NewLine);
            if (!Directory.Exists(tbxFolderPath.Text))
            {
                tbxLog.AppendText("Path '" + tbxFolderPath.Text + "' not found." + Environment.NewLine);
                lblStatus.Text = "Path doesn't exist.";
                return;
            }

            Regex regex = new Regex("^Photos from (?<year>[1-9][0-9][0-9][0-9])$");

            String[] folders = Directory.GetDirectories(tbxFolderPath.Text.TrimEnd('/') + "/");
            foreach (String folder in folders)
            {
                Match match = regex.Match(Path.GetFileName(folder));
                if (match.Success)
                {
                    Tuple<String, String> directory = new Tuple<string, string>(match.Groups["year"].Value, folder);
                    directories.Add(directory);
                }
            }

            if (directories.Count == 0)
            {
                tbxLog.AppendText("Nothing found. We are looking for folder's name starting with 'Photos from YEAR'. You will need rename all folders to match this pattern." + Environment.NewLine);
                lblStatus.Text = "Error";
                return;
            }

            foreach (var directory in directories)
            {
                tbxLog.AppendText("Year: " + directory.Item1 + ", Directory: " + Path.GetFileName(directory.Item2) + Environment.NewLine);
            }

            lblStatus.Text = directories.Count + " folder(s) found. Ready to scan.";
            validated = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "...";
            tbxLog.Clear();
            tbxLog.AppendText("Scan" + Environment.NewLine);
            if (!validated)
            {
                tbxLog.AppendText("Click on validate button before scan." + Environment.NewLine);
                lblStatus.Text = "Error";
                return;
            }

            Dictionary<String, int> extensions = new Dictionary<String, int>();
            List<String> noExtensionWarning = new List<String>();
            foreach (var directory in directories)
            {
                var files = Directory.GetFiles(directory.Item2);
                foreach (var file in files)
                {
                    var extension = Path.GetExtension(file).ToLower();
                    if (extension == "")
                    {
                        noExtensionWarning.Add(file);
                    }
                    else
                    {
                        if (extensions.ContainsKey(extension))
                        {
                            extensions[extension]++;
                        }
                        else
                        {
                            extensions.Add(extension, 1);
                        }
                    }
                }
            }

            foreach (var extension in extensions.Keys)
            {
                tbxLog.AppendText("Type: " + extension + ", Count: " + extensions[extension] + Environment.NewLine);
            }

            if (noExtensionWarning.Count > 0)
            {
                tbxLog.AppendText("The following files doesn't have extension. Remove them manually and scan again." + Environment.NewLine);
                tbxLog.AppendText("You can put 'kind:= -folder type:= -[] extension:= []' in Windows Explorer search field to easily find them." + Environment.NewLine);
                lblStatus.Text = "Error";
                foreach (var file in noExtensionWarning)
                {
                    tbxLog.AppendText(file + Environment.NewLine);
                }
                return;
            }

            lblStatus.Text = "Succesfully scanned. Ready to start.";
            scanned = true;
        }

        bool live_photo_scanned = false;
        List<String> live_photo_files = new List<string>();
        private void button4_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "...";
            live_photo_files.Clear();
            tbxLog.Clear();
            tbxLog.AppendText("Scan for live photos" + Environment.NewLine);
            if (!scanned)
            {
                tbxLog.AppendText("Click on scan button before scan for live photos." + Environment.NewLine);
                lblStatus.Text = "Error";
                return;
            }

            String[] extensions_live_photo = { ".jpg", ".jpeg", ".png", ".heic" };
            tbxLog.AppendText("Looking for match (" + String.Join(",", extensions_live_photo) + ") <> (.mp4) to allow remove video part of live photos." + Environment.NewLine);
            foreach (var directory in directories)
            {
                var files = Directory.GetFiles(directory.Item2);
                foreach (var file in files)
                {
                    var extension = Path.GetExtension(file).ToLower();
                    if (extensions_live_photo.Contains(extension))
                    {
                        var file_mp4 = Path.GetDirectoryName(file) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file) + ".mp4";
                        if (File.Exists(file_mp4))
                        {
                            live_photo_files.Add(file_mp4);
                            tbxLog.AppendText(file + Environment.NewLine);
                        }
                    }
                }
            }

            if (live_photo_files.Count == 0)
            {
                tbxLog.AppendText("Live photos not found." + Environment.NewLine);
                lblStatus.Text = "Error";
                return;
            }

            lblStatus.Text = live_photo_files.Count + " file(s) found. Ready to remove.";
            live_photo_scanned = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "...";
            tbxLog.Clear();
            tbxLog.AppendText("Remove video part of live photos" + Environment.NewLine);
            if (!live_photo_scanned)
            {
                tbxLog.AppendText("Click on scan for live photos button before scan for live photos." + Environment.NewLine);
                lblStatus.Text = "Error";
                return;
            }

            tbxLog.AppendText("Removing " + live_photo_files.Count + " file(s)." + Environment.NewLine);

            foreach (var live_photo_file in live_photo_files)
            {
                tbxLog.AppendText("Removing " + live_photo_file + Environment.NewLine);
                try
                {
                    File.Delete(live_photo_file);
                }
                catch (Exception exc)
                {
                    tbxLog.AppendText(exc.ToString() + Environment.NewLine);
                    lblStatus.Text = "Error.";
                    return;
                }
            }

            live_photo_scanned = false;
            live_photo_files.Clear();

            lblStatus.Text = "Done.";
        }

        bool reorganize_scanned = false;
        private void button5_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "...";
            tbxLog.Clear();
            tbxLog.AppendText("Scan for live reorganize" + Environment.NewLine);
            if (!scanned)
            {
                tbxLog.AppendText("Click on scan button before scan for live photos." + Environment.NewLine);
                lblStatus.Text = "Error";
                return;
            }

            List<string> reorganize_noattribute = new List<string>();
            tbxLog.AppendText("Looking for original date info." + Environment.NewLine);
            foreach (var directory in directories)
            {
                var files = Directory.GetFiles(directory.Item2);
                foreach (var file in files)
                {
                    DateTime? date;
                    try
                    {
                         date = getOriginalDateFromFile(file);
                    }
                    catch(Exception exc)
                    {
                        tbxLog.AppendText("Error getting date info." + Environment.NewLine);
                        tbxLog.AppendText(file + Environment.NewLine);
                        tbxLog.AppendText(exc.ToString() + Environment.NewLine);
                        lblStatus.Text = "Error.";
                        return;
                    }

                    if (date == null)
                    {
                        reorganize_noattribute.Add(file);
                        continue;
                    }
                }
            }

            if (reorganize_noattribute.Count > 0)
            {
                tbxLog.AppendText("The following " + reorganize_noattribute.Count + " file(s) doesn't have original date info." + Environment.NewLine);
                tbxLog.AppendText("You can add this info manually or continue to use Created/Modified date info." + Environment.NewLine);
                lblStatus.Text = "Error";
                foreach (var file in reorganize_noattribute)
                {
                    tbxLog.AppendText(file + Environment.NewLine);
                }
                tbxLog.AppendText("This is only for your information and decision. We recognize HEIC, JPG, JPEG, NEF, PNG, MOV, MP4 types. All GIF and any file without original date taken/created will be processed with Created/Modified date." + Environment.NewLine);
            }

            reorganize_scanned = true;

            lblStatus.Text = "Ready to reorganize.";
        }

        private DateTime? getOriginalDateFromFile(String file)
        {
            string ext = Path.GetExtension(file).ToLower();
            var directories = ImageMetadataReader.ReadMetadata(file);

            if (ext == ".heic" || ext == ".jpg" || ext == ".jpeg")
            {
                //HEIC, JPG, JPEG
                foreach (var sub in directories.OfType<ExifSubIfdDirectory>())
                {
                    if (sub.HasTagName(ExifDirectoryBase.TagDateTimeOriginal) && !String.IsNullOrEmpty(sub.GetDescription(ExifDirectoryBase.TagDateTimeOriginal)))
                    {
                        return sub.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);
                    }
                }
            }
            else if (ext == ".nef")
            {
                //NEF
                foreach (var sub in directories.OfType<ExifIfd0Directory>())
                {
                    if (sub.HasTagName(ExifDirectoryBase.TagDateTimeOriginal) && !String.IsNullOrEmpty(sub.GetDescription(ExifDirectoryBase.TagDateTimeOriginal)))
                    {
                        return sub.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);
                    }
                }

            }
            else if (ext == ".png")
            {
                //PNG
                foreach (var sub in directories.OfType<PngDirectory>())
                {
                    if (sub.HasTagName(PngDirectory.TagTextualData) && !String.IsNullOrEmpty(sub.GetDescription(PngDirectory.TagTextualData)))
                    {
                        return Convert.ToDateTime(sub.GetDescription(PngDirectory.TagTextualData).Replace("Creation Time: ", "").Substring(0, 10).Replace(":", "-"));
                    }
                }

            }
            else if (ext == ".mov" || ext == ".mp4")
            {
                //MOV, MP4
                foreach (var sub in directories.OfType<QuickTimeMovieHeaderDirectory>())
                {
                    if (sub.HasTagName(QuickTimeMovieHeaderDirectory.TagCreated) && !String.IsNullOrEmpty(sub.GetDescription(QuickTimeMovieHeaderDirectory.TagCreated)))
                    {
                        return sub.GetDateTime(QuickTimeMovieHeaderDirectory.TagCreated);
                    }
                }
            }


            //try anyway
            foreach (var sub in directories.OfType<ExifSubIfdDirectory>())
            {
                if (sub.HasTagName(ExifDirectoryBase.TagDateTimeOriginal) && !String.IsNullOrEmpty(sub.GetDescription(ExifDirectoryBase.TagDateTimeOriginal)))
                {
                    return sub.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);
                }
            }

            foreach (var sub in directories.OfType<ExifIfd0Directory>())
            {
                if (sub.HasTagName(ExifDirectoryBase.TagDateTimeOriginal) && !String.IsNullOrEmpty(sub.GetDescription(ExifDirectoryBase.TagDateTimeOriginal)))
                {
                    return sub.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);
                }
            }

            return null;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "...";
            tbxLog.Clear();
            tbxLog.AppendText("Reorganize" + Environment.NewLine);
            if (!reorganize_scanned)
            {
                tbxLog.AppendText("Click on scan for reorganize button before reorganize." + Environment.NewLine);
                lblStatus.Text = "Error";
                return;
            }

            foreach (var directory in directories)
            {
                var files = Directory.GetFiles(directory.Item2);
                foreach (var file in files)
                {
                    DateTime? date;
                    try
                    {
                        date = getOriginalDateFromFile(file);
                    }
                    catch (Exception exc)
                    {
                        tbxLog.AppendText("Error getting date info." + Environment.NewLine);
                        tbxLog.AppendText(file + Environment.NewLine);
                        tbxLog.AppendText(exc.ToString() + Environment.NewLine);
                        lblStatus.Text = "Error.";
                        return;
                    }
                    if (date == null)
                    {
                        FileInfo finfo = new FileInfo(file);
                        date = finfo.CreationTime;
                    }

                    var newdir = directory.Item2 + Path.DirectorySeparatorChar + date.Value.ToString("MM");
                    //use year of date instead of from folder's name. wait: you need comment the code that rename the directory
                    //var newdir = Directory.GetParent(directory.Item2).FullName + Path.DirectorySeparatorChar + date.Value.ToString("yyyy") + Path.DirectorySeparatorChar + date.Value.ToString("MM");
                    if (!Directory.Exists(newdir))
                    {
                        try
                        {
                            Directory.CreateDirectory(newdir);
                        }
                        catch (Exception exc)
                        {
                            tbxLog.AppendText("Error creating directory." + Environment.NewLine);
                            tbxLog.AppendText(newdir + Environment.NewLine);
                            tbxLog.AppendText(exc.ToString() + Environment.NewLine);
                            lblStatus.Text = "Error.";
                            return;
                        }
                    }                     

                    var newfile = newdir + Path.DirectorySeparatorChar + Path.GetFileName(file);                    
                    try
                    {
                        File.Move(file, newfile);
                    }
                    catch (Exception exc)
                    {
                        tbxLog.AppendText("Error moving file." + Environment.NewLine);
                        tbxLog.AppendText(newdir + Environment.NewLine);
                        tbxLog.AppendText(exc.ToString() + Environment.NewLine);
                        lblStatus.Text = "Error.";
                        return;
                    }
                }

                var rename = Directory.GetParent(directory.Item2).FullName + Path.DirectorySeparatorChar + directory.Item1;
                try
                {
                    Directory.Move(directory.Item2, rename);
                }
                catch (Exception exc)
                {
                    tbxLog.AppendText("Error renaming directory." + Environment.NewLine);
                    tbxLog.AppendText("Source dir " + directory.Item2 + Environment.NewLine);
                    tbxLog.AppendText("Destination dir " + rename + Environment.NewLine);
                    tbxLog.AppendText(exc.ToString() + Environment.NewLine);
                    lblStatus.Text = "Error.";
                    return;
                }
            }

            validated = false;
            scanned = false;
            live_photo_scanned = false;
            reorganize_scanned = false;
            lblStatus.Text = "Done.";
        }
    }
}
