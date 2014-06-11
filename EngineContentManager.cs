using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine
{
    public class EngineContentManager
    {
        static Game game;
        static Dictionary<String, Texture2D> textures = new Dictionary<string, Texture2D>();
        static List<String> textureFiles = new List<string>();
        static String currentDirectory = Environment.CurrentDirectory;
        static Microsoft.Xna.Framework.Content.ContentManager XNAcontent;
        static ContentManager LibContent;

        public EngineContentManager(Game game, ContentManager XNAContent)
        {
            EngineContentManager.game = game;
            LoadTextureContent();
            EngineContentManager.XNAcontent = XNAContent;

            LibContent = new ContentManager(game.Services);
            LibContent.RootDirectory = "Content";
        }

        public static Microsoft.Xna.Framework.Content.ContentManager GetXNAContent()
        {
            return EngineContentManager.XNAcontent;
        }

        public void LoadTextureContent()
        {
            

            if (!System.IO.Directory.Exists(currentDirectory + "/textures"))
                System.IO.Directory.CreateDirectory(currentDirectory + "/textures");

            
            foreach (String file in Directory.GetFiles(currentDirectory + "\\textures"))
            {
                textureFiles.Add(file);
                Console.Out.WriteLine(file);
            }
        }

        public static  void LoadSceneContent(Scene scene, GraphicsDevice graphics)
        {
            FileStream fileStream;

            foreach (String file in textureFiles)
            {
                String temp = file.Substring(currentDirectory.Length);
                foreach (String contentName in scene.SceneContent)
                {
                    if (temp.Contains(contentName) && !textures.ContainsKey(contentName))
                    {
                        fileStream = new FileStream(file, FileMode.Open);
                        textures.Add(contentName, Texture2D.FromStream(graphics, fileStream));
                        fileStream.Close();
                    }    
                }                
            }

            //textures.Add(contentName, game.Content.Load<Texture2D>(contentName));       
        }

        public static void LoadSceneTexture(Scene scene, String textureName)
        {
            FileStream fileStream;

            foreach (String file in textureFiles)
            {
                String temp = file.Substring(currentDirectory.Length);
                if (temp.Contains(textureName) && !textures.ContainsKey(textureName))
                {
                    fileStream = new FileStream(file, FileMode.Open);
                    textures.Add(textureName, Texture2D.FromStream(game.GraphicsDevice, fileStream));
                    fileStream.Close();
                }          
            }           
        }

        public static Texture2D GetTexture(String name)
        {
            if(textures.ContainsKey(name))
                return textures[name];
            return LibContent.Load<Texture2D>("Error");
        }
    }
}
