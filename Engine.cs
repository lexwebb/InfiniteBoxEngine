using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using InfiniteBoxEngine.Animation.Skeletal;
using InfiniteBoxEngine.GUI.Controls;
using InfiniteBoxEngine.Object;
using InfiniteBoxEngine.Skeletal.Animation;
using FarseerPhysics.Dynamics;
using InfiniteBoxEngine.GUI.Menu;

namespace InfiniteBoxEngine {
    public class Engine {
        static Game game;
        static GraphicsDeviceManager graphics;
        static ControlManager controlManager;
        static EngineContentManager contentManager;
        static GUIManager guiManager;
        static SpriteBatch gameSpriteBatch;
        static SpriteBatch uiSpriteBatch;
        static Gameworld gameworld;
        static Menu currentMenu;

        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;
        static TimeSpan totalGameTime;

        static float PHYSICS_STEP = 1f / 60f;

        public Engine(Game aGame, GraphicsDeviceManager aGraphics) {
            game = aGame;
            graphics = aGraphics;
            controlManager = new ControlManager(Mouse.GetState(), Keyboard.GetState());
            contentManager = new EngineContentManager(game, game.Content);
            guiManager = new GUIManager();
            gameSpriteBatch = new SpriteBatch(aGraphics.GraphicsDevice);
            uiSpriteBatch = new SpriteBatch(aGraphics.GraphicsDevice);
            gameworld = new Gameworld(aGame);
        }

        public void Initialize() {
            game.Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
            gameworld.Camera.Focus = Vector2.Zero;
            gameworld.Camera.TargetScale = 1;
            gameworld.Camera.Initialize();
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e) {
            Console.Out.WriteLine("Resized: " + game.Window.ClientBounds.ToString());
            gameworld.Camera.Initialize();
        }

        public void Update(GameTime gameTime) {
            totalGameTime = gameTime.TotalGameTime;
            HandleInput();
            guiManager.UpdateGUI(controlManager);

            if (currentMenu != null)
                currentMenu.UpdateMenu(controlManager);

            if (gameworld.Running)
                gameworld.World.Step(PHYSICS_STEP);


            gameworld.Camera.Update(gameTime);
        }

        public void OpenMenu(Menu menu) {
            this.CurrentMenu = menu;
            gameworld.Running = false;
        }

        public void CloseMenu(Menu menu) {
            gameworld.Running = true;
        }

        public void LoadContent() {
            //Button button = new Button("btn_Test", new Vector2(10f, 10f), 100, 40,
            //ContentManager.GetTexture("btn"), ContentManager.GetTexture("btn2"), ContentManager.GetTexture("btn"));

            gameworld.Camera.Focus = Vector2.Zero;
            gameworld.Camera.TargetScale = 1;
            gameworld.Camera.Initialize();

            //if (gameworld.CurrentScene != null)
            //    gameworld.CurrentScene.LoadContent();
        }

        private void HandleInput() {
            controlManager.UpdateMouseState(Mouse.GetState());
            controlManager.UpdateKeyboardState(Keyboard.GetState());

            if (gameworld.CurrentScene != null && gameworld.Running)
                gameworld.CurrentScene.HandleInput(controlManager);
        }

        public void Draw(GameTime gameTime) {
            #region GameWorld
            #region GameObjects
            gameSpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, gameworld.Camera.Transform);

            if (gameworld.CurrentScene != null)
                foreach (GameObject gameObject in gameworld.CurrentScene.GameObjects) {
                    gameObject.Draw(gameworld.Camera, gameSpriteBatch, gameTime);
                }

            Gameworld.CurrentScene.Draw();

            gameSpriteBatch.End();
            #endregion

            #region Foreground
            gameSpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, gameworld.Camera.Transform);

            Gameworld.CurrentScene.DrawForeground();

            gameSpriteBatch.End();
            #endregion
            #endregion

            #region UI
            #region Background
            uiSpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null);

            guiManager.DrawGUIBackground(uiSpriteBatch, gameTime);

            uiSpriteBatch.End();
            #endregion

            #region Middleground
            uiSpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null);

            guiManager.DrawGUIMiddleground(uiSpriteBatch, gameTime);

            uiSpriteBatch.End();
            #endregion

            #region Foreground
            uiSpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null);

            guiManager.DrawGUIForeground(uiSpriteBatch, gameTime);

            uiSpriteBatch.End();
            #endregion

            #region Highlights
            uiSpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null);

            guiManager.DrawGUIHighlights(uiSpriteBatch, gameTime);

            uiSpriteBatch.End();
            #endregion
            #endregion

            //if (currentMenu != null)
            //    currentMenu.DrawMenu(uiSpriteBatch, gameTime);
        }

        public static GraphicsDevice GetGraphicsDevice() {
            return graphics.GraphicsDevice;
        }

        public Menu CurrentMenu {
            get { return Engine.currentMenu; }
            set { Engine.currentMenu = value; }
        }

        //public Gameworld Gameworld
        //{
        //    get { return Engine.gameworld; }
        //    set { Engine.gameworld = value; }
        //}

        public static Gameworld Gameworld {
            get { return Engine.gameworld; }
            internal set { Engine.gameworld = value; }
        }

        public static GUIManager GuiManager {
            get { return Engine.guiManager; }
            internal set { Engine.guiManager = value; }
        }

        public static ControlManager ControlManager {
            get { return Engine.controlManager; }
            internal set { Engine.controlManager = value; }
        }

        public static TimeSpan GetGameTime() {
            return totalGameTime;
        }

        public static SpriteBatch GameSpriteBatch { get { return Engine.gameSpriteBatch; } }
        public static SpriteBatch UISpriteBatch { get { return Engine.uiSpriteBatch; } }

        public void DrawCalcFPS(GameTime gameTime) {
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1)) {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }

            frameCounter++;
        }
    }
}
