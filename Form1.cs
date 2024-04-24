using System;
using System.Reflection.Metadata;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace MethaneNew
{
    public partial class Form1 : Form
    {
        static string? SDLPath;
        static string? SDLPathImage;
        static string? sdlLibPath;
        static string? sdlLibPathImage;
        static string? sdlIncludePath;
        static string? sdlIncludePathImage;
        static string? sdlFilePath;
        static string? sdlFilePath2;
        static string? sdlFilePath3;
        static string? sdlFilePath4;
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

                        File.WriteAllText(headerFileName, CreateGameString());
                        File.WriteAllText(cppFileName, CreateGameDefString());
                        File.WriteAllText(cppMainFileName, CreateMainString());

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

                        File.WriteAllText(headerFileName, CreateClassString());
                        File.WriteAllText(cppFileName, CreateClassDefString());

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
                //ConnectVC(doc, sdlIncludePathImage! + ";$(IncludePath)", sdlLibPathImage! + ";$(LibraryPath)", nsmgr);
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


        public string CreateClassString()
        {
            StringBuilder MainString = new StringBuilder();
            MainString.AppendLine("#pragma once");
            MainString.AppendLine("#include <iostream>");
            MainString.AppendLine("#include <SDL.h>");
            MainString.AppendLine("");
            MainString.AppendLine("");
            MainString.AppendLine("class " + textBoxClass.Text);
            MainString.AppendLine("{");
            MainString.AppendLine("    private:");
            MainString.AppendLine("        SDL_Texture * texture = nullptr;");
            MainString.AppendLine("        SDL_Rect rectangle;");
            MainString.AppendLine("");
            MainString.AppendLine("    public:");
            MainString.AppendLine("        SDL_Texture *GetTexture();");
            MainString.AppendLine("        void SetTexture(SDL_Texture* temptex);");
            MainString.AppendLine("        SDL_Rect* GetRectangle();");
            MainString.AppendLine("        void Render(SDL_Renderer* renderer);");
            MainString.AppendLine("};");

            return MainString.ToString();

        }

        public string CreateClassDefString()
        {
            StringBuilder MainString = new StringBuilder();
            MainString.AppendLine("#include <iostream>");
            MainString.AppendLine("#include <SDL.h>");
            MainString.AppendLine("#include \"" + textBoxClass.Text + ".h\"");
            MainString.AppendLine("SDL_Texture* " + textBoxClass.Text + "::GetTexture() {");
            MainString.AppendLine("    return texture;");
            MainString.AppendLine("}");
            MainString.AppendLine("");
            MainString.AppendLine("void " + textBoxClass.Text + "::SetTexture(SDL_Texture * temptex) {");
            MainString.AppendLine("    texture = temptex;");
            MainString.AppendLine("}");
            MainString.AppendLine("");
            MainString.AppendLine("SDL_Rect* " + textBoxClass.Text + "::GetRectangle() {");
            MainString.AppendLine("    return &rectangle;");
            MainString.AppendLine("}");
            MainString.AppendLine("");
            MainString.AppendLine("void " + textBoxClass.Text + "::Render(SDL_Renderer * renderer) {");
            MainString.AppendLine("    SDL_RenderCopy(renderer, texture, NULL, &rectangle);");
            MainString.AppendLine("}");

            return MainString.ToString();
        }

        public string CreateMainString()
        {
            StringBuilder MainString = new StringBuilder();
            MainString.AppendLine("#include <iostream>");
            MainString.AppendLine("#include \"" + textBoxSkeleton.Text + ".h\"");
            MainString.AppendLine("");
            MainString.AppendLine("");
            MainString.AppendLine("int main(int argv, char* argc[])");
            MainString.AppendLine("{");
            MainString.AppendLine("    bool status = true;");
            MainString.AppendLine("    " + textBoxSkeleton.Text + "* " + textBoxSkeleton.Text.ToLower() + " = new " + textBoxSkeleton.Text + "();");
            MainString.AppendLine("");
            MainString.AppendLine("    " + textBoxSkeleton.Text.ToLower() + "->Start();");
            MainString.AppendLine("");
            MainString.AppendLine("    while (status)");
            MainString.AppendLine("    {");
            MainString.AppendLine("        " + textBoxSkeleton.Text.ToLower() + "->Movement(status);");
            MainString.AppendLine("        " + textBoxSkeleton.Text.ToLower() + "->Events();");
            MainString.AppendLine("        " + textBoxSkeleton.Text.ToLower() + "->Render();");
            MainString.AppendLine("");
            MainString.AppendLine("        SDL_Delay(16);");
            MainString.AppendLine("    }");
            MainString.AppendLine("");
            MainString.AppendLine("    delete " + textBoxSkeleton.Text.ToLower() + ";");
            MainString.AppendLine("    return 0;");
            MainString.AppendLine("}");
            return MainString.ToString();

        }
        public string CreateGameString()
        {
            StringBuilder MainString = new StringBuilder();
            MainString.AppendLine("#pragma once");
            MainString.AppendLine("#include <SDL.h>");
            MainString.AppendLine("");
            MainString.AppendLine("class " + textBoxSkeleton.Text + " {");
            MainString.AppendLine("private:");
            MainString.AppendLine("    SDL_Window* window;");
            MainString.AppendLine("    SDL_Renderer* renderer;");
            MainString.AppendLine("");
            MainString.AppendLine("public:");
            MainString.AppendLine("    " + textBoxSkeleton.Text + "();");
            MainString.AppendLine("");
            MainString.AppendLine("    void Start();");
            MainString.AppendLine("");
            MainString.AppendLine("    void LoadTextures();");
            MainString.AppendLine("");
            MainString.AppendLine("    void Events();");
            MainString.AppendLine("");
            MainString.AppendLine("    void Exit(bool& status, const Uint8* state);");
            MainString.AppendLine("");
            MainString.AppendLine("    void Movement(bool& status);");
            MainString.AppendLine("");
            MainString.AppendLine("    void Render();");
            MainString.AppendLine("");
            MainString.AppendLine("    SDL_Texture* load(const char* file, SDL_Renderer* ren);");
            MainString.AppendLine("");
            MainString.AppendLine("    ~" + textBoxSkeleton.Text + "();");
            MainString.AppendLine("};");
            return MainString.ToString();
        }

        public string CreateGameDefString()
        {
            StringBuilder MainString = new StringBuilder();
            MainString.AppendLine("#include <SDL.h>");
            MainString.AppendLine("#include <iostream>");
            MainString.AppendLine("#include \"SDL_image.h\"");
            MainString.AppendLine("");
            MainString.AppendLine("#include \"" + textBoxSkeleton.Text + ".h\"");
            MainString.AppendLine("");
            MainString.AppendLine("" + textBoxSkeleton.Text + "::" + textBoxSkeleton.Text + "() {");
            MainString.AppendLine("    window = nullptr;");
            MainString.AppendLine("    renderer = nullptr;");
            MainString.AppendLine("}");
            MainString.AppendLine("");
            MainString.AppendLine("void " + textBoxSkeleton.Text + "::Start() {");
            MainString.AppendLine("    SDL_Init(SDL_INIT_EVERYTHING);");
            MainString.AppendLine("    window = SDL_CreateWindow(\"Window\", SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, 1000, 800, SDL_WINDOW_SHOWN);");
            MainString.AppendLine("    renderer = SDL_CreateRenderer(window, -1, 0);");
            MainString.AppendLine("");
            MainString.AppendLine("    SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);");
            MainString.AppendLine("    LoadTextures();");
            MainString.AppendLine("");
            MainString.AppendLine("}");
            MainString.AppendLine("");
            MainString.AppendLine("void " + textBoxSkeleton.Text + "::LoadTextures() {");
            MainString.AppendLine("    //example->SetTexture(load(\"textures/example.png\", renderer)); Example Texture Load");
            MainString.AppendLine("}");
            MainString.AppendLine("");
            MainString.AppendLine("void " + textBoxSkeleton.Text + "::Events() {");
            MainString.AppendLine("}");
            MainString.AppendLine("");
            MainString.AppendLine("void " + textBoxSkeleton.Text + "::Exit(bool& status, const Uint8* state) {");
            MainString.AppendLine("    if (state[SDL_SCANCODE_ESCAPE]) {");
            MainString.AppendLine("        status = false;");
            MainString.AppendLine("    }");
            MainString.AppendLine("}");
            MainString.AppendLine("");
            MainString.AppendLine("void " + textBoxSkeleton.Text + "::Movement(bool &status) {");
            MainString.AppendLine("    SDL_PumpEvents();");
            MainString.AppendLine("    const Uint8* state = SDL_GetKeyboardState(NULL);");
            MainString.AppendLine("    Exit(status,state);");
            MainString.AppendLine("}");
            MainString.AppendLine("");
            MainString.AppendLine("void " + textBoxSkeleton.Text + "::Render() {");
            MainString.AppendLine("    SDL_RenderClear(renderer);");
            MainString.AppendLine("    //SDL_RenderCopy(renderer, textback, NULL, &rectback); Example Direct Rendering");
            MainString.AppendLine("    SDL_RenderPresent(renderer);");
            MainString.AppendLine("}");
            MainString.AppendLine("");
            MainString.AppendLine("SDL_Texture* " + textBoxSkeleton.Text + "::load(const char* file, SDL_Renderer* ren) {");
            MainString.AppendLine("    SDL_Surface* tmpSurface = IMG_Load(file);");
            MainString.AppendLine("    SDL_Texture* tex = SDL_CreateTextureFromSurface(ren, tmpSurface);");
            MainString.AppendLine("    SDL_FreeSurface(tmpSurface);");
            MainString.AppendLine("    return tex;");
            MainString.AppendLine("}");
            MainString.AppendLine("");
            MainString.AppendLine("" + textBoxSkeleton.Text + "::~" + textBoxSkeleton.Text + "() {");
            MainString.AppendLine("    SDL_DestroyRenderer(renderer);");
            MainString.AppendLine("    SDL_DestroyWindow(window);");
            MainString.AppendLine("    SDL_Quit();");
            MainString.AppendLine("    //std::cout << \"Resources Destroyed\";");
            MainString.AppendLine("}");
            return MainString.ToString();
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