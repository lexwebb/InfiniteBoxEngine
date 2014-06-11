using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine
{
    public class Gameworld
    {
        static World world;
        static Scene currentScene;
        static Serialiser serialiser = new Serialiser();
        static Camera2D camera;
        Game game;

        static float PHYSICS_STEP = 1f / 60f;
        static float GRAVITY = -98.1f;

        public Gameworld(Game game)
        {
            this.game = game;
            world = new World(new Vector2(0f, GRAVITY));
            camera = new Camera2D(game);
            Running = true;

            //Set display to simulation display ratio
            ConvertUnits.SetDisplayUnitToSimUnitRatio(10f);
            
            //LoadScene("Test");

            //currentScene = new Scene("Test");

            //currentScene.SceneContent.Add("tex_Crate.png");
            //currentScene.SceneContent.Add("CheckboxChecked.bmp");
            //currentScene.SceneContent.Add("CheckboxUnChecked.bmp");

            //ContentManager.LoadSceneContent(currentScene, game.GraphicsDevice);

            //currentScene.GameObjects.Add(new Crate(world, "crate", new Vector2(100f, 100f), BodyType.Dynamic));
            //currentScene.GameObjects.Add(new Crate(world, "crate2", new Vector2(105f, 80f), BodyType.Dynamic));
            //currentScene.GameObjects.Add(new FixedPlane(world, "floor", new Vector2(-1000, 300), new Vector2(3000, 300)));

            //serialiser.SaveScene(currentScene);
        }

        public World World
        {
            get { return world; }
            set { world = value; }
        }

        public Scene CurrentScene
        {
            get { return currentScene; }
            set { currentScene = value; InitializeCurrentScene(); }
        }

        public Camera2D Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        public bool Running { get; set; }

        public void InitializeCurrentScene()
        {
            EngineContentManager.LoadSceneContent(currentScene, game.GraphicsDevice);
            Engine.GuiManager.ClearControls();
            Engine.Gameworld.World.Clear();
            currentScene.LoadContent(); 
        }

        public void LoadScene(String sceneName)
        {
            currentScene = serialiser.LoadScene(sceneName);
            EngineContentManager.LoadSceneContent(currentScene, game.GraphicsDevice);
            currentScene.CreateNullBodys(world);
            currentScene.UpdateNullTextures();
        }
    }
}
