using System;
using System.Reflection.Metadata;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace MethaneNew
{
    public partial class Form1 : Form
    {
        static string? SDLPath;
        static string? sdlIncludePath;
        static string? sdlLibPath;

        static string? SDLPathImage;
        static string? sdlLibPathImage;
        static string? sdlIncludePathImage;

        static string? SDLPathMixer;
        static string? sdlLibPathMixer;
        static string? sdlIncludePathMixer;

        static string? sdlFilePath;
        static string? sdlFilePath2;
        static string? sdlFilePath3;
        static string? sdlFilePath4;
        static string? sdlFilePath5;
        static string? projectPath;
        static string? projectDirectoryPath;
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnSkeletonCreate_Click(object sender, EventArgs e)
        {
            if (textBoxSkeleton.Text != "")
            {
                try
                {
                    SkeletonDialog.DefaultExt = "cpp";
                    ClassDialog.FileName = textBoxSkeleton.Text + ".cpp";
                    if (ClassDialog.ShowDialog() == DialogResult.OK)
                    {
                        string baseDirectory = Path.GetDirectoryName(ClassDialog.FileName)!;
                        string headerFileName = Path.Combine(baseDirectory, textBoxSkeleton.Text + ".h");
                        string cppFileName = Path.Combine(baseDirectory, textBoxSkeleton.Text + ".cpp");
                        string cppMainFileName = Path.Combine(baseDirectory, "main.cpp");

                        File.WriteAllText(headerFileName, CreateGameDefString(textBoxSkeleton.Text));
                        File.WriteAllText(cppFileName, CreateGameString(textBoxSkeleton.Text));
                        File.WriteAllText(cppMainFileName, CreateMainString(textBoxSkeleton.Text));

                        MessageBox.Show("Files created successfully!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Class name cannot be empty!");
            }

        }

        private void BtnClassCreate_Click(object sender, EventArgs e)
        {
            if (textBoxClass.Text != "")
            {
                try
                {
                    ClassDialog.DefaultExt = "h";
                    ClassDialog.FileName = textBoxClass.Text + ".h";
                    if (ClassDialog.ShowDialog() == DialogResult.OK)
                    {
                        string baseDirectory = Path.GetDirectoryName(ClassDialog.FileName)!;
                        string headerFileName = Path.Combine(baseDirectory, textBoxClass.Text + ".h");
                        string cppFileName = Path.Combine(baseDirectory, textBoxClass.Text + ".cpp");

                        File.WriteAllText(headerFileName, CreateClassDefString(textBoxClass.Text));
                        File.WriteAllText(cppFileName, CreateClassString(textBoxClass.Text));

                        MessageBox.Show("Files created successfully!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Class name cannot be empty!");
            }
        }

        private void BtnLink_Click(object sender, EventArgs e)
        {
            if (checkBox32.Checked == false && checkBox64.Checked == false)
            {
                MessageBox.Show("You need to choose 32 or 64 bit version");
            }
            else
            {
                MessageBox.Show("Select SDL2 Location");

                var dialogResult = SDLDialog.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    SDLPath = SDLDialog.SelectedPath;
                    string sdltempPath = Path.Combine(SDLPath, "lib");

                    if (checkBox64.Checked == false) { sdlLibPath = Path.Combine(sdltempPath, "x86"); }

                    else { sdlLibPath = Path.Combine(sdltempPath, "x64"); }

                    sdlFilePath = Path.Combine(sdlLibPath, "SDL2.dll");

                    sdlIncludePath = Path.Combine(SDLPath, "include");

                    MessageBox.Show("SDL loaded");
                }
                else
                {
                    MessageBox.Show("No path selected.");
                    return;
                }
                if (checkBoxImage.Checked == true)
                {
                    MessageBox.Show("Now select sdl image location");
                    var dialogResult2 = SDLDialog.ShowDialog();

                    if (dialogResult2 == DialogResult.OK)
                    {
                        SDLPathImage = SDLDialog.SelectedPath;
                        string sdltempPath = Path.Combine(SDLPathImage, "lib");

                        if (checkBox64.Checked == false) { sdlLibPathImage = Path.Combine(sdltempPath, "x86"); }

                        else { sdlLibPathImage = Path.Combine(sdltempPath, "x64"); }

                        sdlFilePath2 = Path.Combine(sdlLibPathImage, "SDL2_image.dll");
                        sdlFilePath3 = Path.Combine(sdlLibPathImage, "libpng16-16.dll");
                        sdlFilePath4 = Path.Combine(sdlLibPathImage, "zlib1.dll");

                        sdlIncludePathImage = Path.Combine(SDLPathImage, "include");

                        MessageBox.Show("SDL Image loaded");
                    }
                    else
                    {
                        MessageBox.Show("No path selected.");
                        return;
                    }

                }

                if (checkBoxMixer.Checked == true)
                {
                    MessageBox.Show("Now select sdl mixer location");
                    var dialogResult3 = SDLDialog.ShowDialog();

                    if (dialogResult3 == DialogResult.OK)
                    {
                        SDLPathMixer = SDLDialog.SelectedPath;
                        string sdltempPath = Path.Combine(SDLPathMixer, "lib");

                        if (checkBox64.Checked == false) { sdlLibPathMixer = Path.Combine(sdltempPath, "x86"); }

                        else { sdlLibPathMixer = Path.Combine(sdltempPath, "x64"); }

                        sdlFilePath5 = Path.Combine(sdlLibPathMixer, "SDL2_mixer.dll");

                        sdlIncludePathMixer = Path.Combine(SDLPathMixer, "include");

                        MessageBox.Show("SDL Mixer loaded");
                    }
                    else
                    {
                        MessageBox.Show("No path selected.");
                        return;
                    }

                }

                MessageBox.Show("now select vcxproj file");
                SDLOpenDialog.Filter = "Visual Studio C++ project (*.vcxproj)|*.vcxproj";
                var result = SDLOpenDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    SDLInitialize();
                }
                else
                {
                    MessageBox.Show("No project file selected.");
                }
            }


        }

        void SDLInitialize()
        {
            projectPath = SDLOpenDialog.FileName;
            projectDirectoryPath = Path.GetDirectoryName(projectPath)!;
            XmlDocument doc = new XmlDocument();
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("msb", "http://schemas.microsoft.com/developer/msbuild/2003");


            ConnectInclude(doc, sdlIncludePath!, nsmgr);

            ConnectLibraries(doc, sdlLibPath!, nsmgr);

            ConnectDependencies(doc, "SDL2.lib;SDL2main.lib", nsmgr);

            if (checkBoxImage.Checked == true)
            {
                ConnectInclude(doc, sdlIncludePathImage!, nsmgr);
                ConnectLibraries(doc, sdlLibPathImage!, nsmgr);
                ConnectDependencies(doc, "SDL2_image.lib", nsmgr);
            }
            if (checkBoxMixer.Checked == true)
            {
                ConnectInclude(doc, sdlIncludePathMixer!, nsmgr);
                ConnectLibraries(doc, sdlLibPathMixer!, nsmgr);
                ConnectDependencies(doc, "SDL2_mixer.lib", nsmgr);
            }

            ConnectInclude(doc, "%(AdditionalIncludeDirectories)", nsmgr);
            ConnectLibraries(doc, "%(AdditionalLibraryDirectories)", nsmgr);
            ConnectDependencies(doc, "%(AdditionalDependencies)", nsmgr);

            string destinationFilePath = Path.Combine(projectDirectoryPath!, "SDL2.dll");
            File.Copy(sdlFilePath!, destinationFilePath, overwrite: true);

            if (checkBoxImage.Checked == true)
            {
                string filePath1 = Path.Combine(projectDirectoryPath!, "SDL2_image.dll");
                string filePath2 = Path.Combine(projectDirectoryPath!, "libpng16-16.dll");
                string filePath3 = Path.Combine(projectDirectoryPath!, "zlib1.dll");
                File.Copy(sdlFilePath2!, filePath1, overwrite: true);
                File.Copy(sdlFilePath3!, filePath2, overwrite: true);
                File.Copy(sdlFilePath4!, filePath3, overwrite: true);
            }
            if (checkBoxMixer.Checked == true)
            {
                string filePath1 = Path.Combine(projectDirectoryPath!, "SDL2_mixer.dll");
                File.Copy(sdlFilePath5!, filePath1, overwrite: true);

            }

            MessageBox.Show("SDL linked to the project");
        }

        void ConnectInclude(XmlDocument doc, string content, XmlNamespaceManager nsmgr)
        {
            try
            {
                doc.Load(projectPath!);
                XmlNodeList clCompileNodes = doc.SelectNodes("//msb:Project/msb:ItemDefinitionGroup/msb:ClCompile", nsmgr)!;
                foreach (XmlNode node in clCompileNodes!)
                {
                    XmlNode additionalIncludesNode = node.SelectSingleNode("msb:AdditionalIncludeDirectories", nsmgr)!;
                    if (additionalIncludesNode != null)
                    {
                        additionalIncludesNode.InnerText += $";{content}";
                    }
                    else
                    {
                        XmlElement newElement = doc.CreateElement("AdditionalIncludeDirectories", nsmgr.LookupNamespace("msb"));
                        newElement.InnerText = content;
                        node.AppendChild(newElement);
                    }
                }
                doc.Save(projectPath!);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to modify project:" + ex.Message);
            }
        }


        void ConnectLibraries(XmlDocument doc, string content, XmlNamespaceManager nsmgr)
        {
            try
            {
                doc.Load(projectPath!);
                XmlNodeList linkNodes = doc.SelectNodes("//msb:Project/msb:ItemDefinitionGroup/msb:Link", nsmgr)!;
                foreach (XmlNode node in linkNodes!)
                {
                    XmlNode additionalLibDirsNode = node.SelectSingleNode("msb:AdditionalLibraryDirectories", nsmgr)!;
                    if (additionalLibDirsNode != null)
                    {
                        additionalLibDirsNode.InnerText += $";{content}";
                    }
                    else
                    {
                        XmlElement newElement = doc.CreateElement("AdditionalLibraryDirectories", nsmgr.LookupNamespace("msb"));
                        newElement.InnerText = content;
                        node.AppendChild(newElement);
                    }
                }
                doc.Save(projectPath!);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to modify project: " + ex.Message);
            }
        }


        void ConnectDependencies(XmlDocument doc, string content, XmlNamespaceManager nsmgr)
        {
            try
            {
                doc.Load(projectPath!);

                XmlNodeList linkNodes = doc.SelectNodes("//msb:Project/msb:ItemDefinitionGroup/msb:Link", nsmgr)!;
                foreach (XmlNode node in linkNodes!)
                {
                    XmlNode additionalDepsNode = node.SelectSingleNode("msb:AdditionalDependencies", nsmgr)!;
                    if (additionalDepsNode != null)
                    {
                        additionalDepsNode.InnerText += $";{content}";
                    }
                    else
                    {
                        XmlElement newElement = doc.CreateElement("AdditionalDependencies", nsmgr.LookupNamespace("msb"));
                        newElement.InnerText = content;
                        node.AppendChild(newElement);
                    }
                }
                doc.Save(projectPath!);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to modify project: " + ex.Message);
            }
        }


        public string CreateClassString(string text)
        {
            string filePath = "Code/MClassM.cpp";

            string content = File.ReadAllText(filePath);

            string updatedContent = content.Replace("MClassM", text);

            return updatedContent;
        }

        public string CreateClassDefString(string text)
        {
            string filePath = "Code/MClassM.h";

            string content = File.ReadAllText(filePath);

            string updatedContent = content.Replace("MClassM", text);

            return updatedContent;
        }

        public string CreateMainString(string text)
        {
            string filePath = "Code/main.cpp";

            string content = File.ReadAllText(filePath);

            string updatedContent = content.Replace("MClassM", text);

            string newContent = updatedContent.Replace("mclassm", text.ToLower());

            return newContent;

        }
        public string CreateGameString(string text)
        {
            string filePath = "Code/MGameClassM.cpp";

            string content = File.ReadAllText(filePath);

            string updatedContent = content.Replace("MClassM", text);

            return updatedContent;
        }

        public string CreateGameDefString(string text)
        {
            string filePath = "Code/MGameClassM.h";

            string content = File.ReadAllText(filePath);

            string updatedContent = content.Replace("MClassM", text);

            return updatedContent;
        }


        private void checkBox32_CheckedChanged(object sender, EventArgs e)
        {
            checkBox64.Checked = false;
        }

        private void checkBox64_CheckedChanged(object sender, EventArgs e)
        {
            checkBox32.Checked = false;
        }
    }
}