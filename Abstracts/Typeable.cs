using InfiniteBoxEngine.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine.Abstracts
{
    public class Typeable
    {
        String text;
        int cursorPosition;
        Vector2 screenPos;
        bool usable = true;
        SpriteFont font;
        Dictionary<Keys, double> lastPressed = new Dictionary<Keys, double>();

        public Typeable(string text, SpriteFont font, Vector2 screenPos)
        {
            this.text = text;
            this.font = font;
            this.screenPos = screenPos;
            Engine.ControlManager.KeyPressEvent += OnKeyPress;
        }

        public Vector2 GetCursorPosition()
        {
            return new Vector2(screenPos.X + font.MeasureString(text.Substring(0, cursorPosition)).X, screenPos.Y);
        }

        public void OnKeyPress(object sender, KeyPressEventArgs args)
        {
            if(usable)
                if(lastPressed.ContainsKey(args.Key))
                {
                    if (lastPressed[args.Key] + 100 < Engine.GetGameTime().TotalMilliseconds)
                    {
                        TypeKey(args.Key, args.KeyboardState);
                        lastPressed[args.Key] = args.TimeInMilliseconds;
                    }
                }
                else 
                {
                    TypeKey(args.Key, args.KeyboardState);
                    lastPressed.Add(args.Key, args.TimeInMilliseconds);
                }           
        }

        public void TypeKey(Keys key, KeyboardState state){
            switch (key)
            {
                case Keys.Left: if (cursorPosition > 0) cursorPosition--; break;
                case Keys.Right: if (cursorPosition < text.Length) cursorPosition++; break;
                case Keys.Delete: if (cursorPosition < text.Length) text = text.Remove(cursorPosition, 1); break;
                case Keys.Back: if (cursorPosition > 0) { text = text.Remove(cursorPosition - 1, 1); cursorPosition--; }; break;

                default:
                    char aChar = TryConvertKey(key, state);
                    if(aChar != '\0')
                        InsertCharAt(aChar, cursorPosition);
                    break;
            }         
        }

        public void InsertCharAt(char aChar, int position){
            text = text.Insert(position, aChar.ToString());
            if(cursorPosition < text.Length)
                cursorPosition++;
        }

        public static char TryConvertKey(Keys key, KeyboardState state)
        {
            bool shift = state.IsKeyDown(Keys.LeftShift);
            switch (key)
            {
                //Alphabet keys
                case Keys.A: if (shift) { return 'A'; } else { return 'a'; };
                case Keys.B: if (shift) { return 'B'; } else { return 'b'; };
                case Keys.C: if (shift) { return 'C'; } else { return 'c'; };
                case Keys.D: if (shift) { return 'D'; } else { return 'd'; };
                case Keys.E: if (shift) { return 'E'; } else { return 'e'; };
                case Keys.F: if (shift) { return 'F'; } else { return 'f'; };
                case Keys.G: if (shift) { return 'G'; } else { return 'g'; };
                case Keys.H: if (shift) { return 'H'; } else { return 'h'; };
                case Keys.I: if (shift) { return 'I'; } else { return 'i'; };
                case Keys.J: if (shift) { return 'J'; } else { return 'j'; };
                case Keys.K: if (shift) { return 'K'; } else { return 'k'; };
                case Keys.L: if (shift) { return 'L'; } else { return 'l'; };
                case Keys.M: if (shift) { return 'M'; } else { return 'm'; };
                case Keys.N: if (shift) { return 'N'; } else { return 'n'; };
                case Keys.O: if (shift) { return 'O'; } else { return 'o'; };
                case Keys.P: if (shift) { return 'P'; } else { return 'p'; };
                case Keys.Q: if (shift) { return 'Q'; } else { return 'q'; };
                case Keys.R: if (shift) { return 'R'; } else { return 'r'; };
                case Keys.S: if (shift) { return 'S'; } else { return 's'; };
                case Keys.T: if (shift) { return 'T'; } else { return 't'; };
                case Keys.U: if (shift) { return 'U'; } else { return 'u'; };
                case Keys.V: if (shift) { return 'V'; } else { return 'v'; };
                case Keys.W: if (shift) { return 'W'; } else { return 'w'; };
                case Keys.X: if (shift) { return 'X'; } else { return 'x'; };
                case Keys.Y: if (shift) { return 'Y'; } else { return 'y'; };
                case Keys.Z: if (shift) { return 'Z'; } else { return 'z'; };

                //Decimal keys
                case Keys.D0: if (shift) { return ')'; } else { return '0'; };
                case Keys.D1: if (shift) { return '!'; } else { return '1'; };
                case Keys.D2: if (shift) { return '@'; } else { return '2'; };
                case Keys.D3: if (shift) { return '#'; } else { return '3'; };
                case Keys.D4: if (shift) { return '$'; } else { return '4'; };
                case Keys.D5: if (shift) { return '%'; } else { return '5'; };
                case Keys.D6: if (shift) { return '^'; } else { return '6'; };
                case Keys.D7: if (shift) { return '&'; } else { return '7'; };
                case Keys.D8: if (shift) { return '*'; } else { return '8'; };
                case Keys.D9: if (shift) { return '('; } else { return '9'; };

                //Decimal numpad keys
                case Keys.NumPad0: return '0';;
                case Keys.NumPad1: return '1';;
                case Keys.NumPad2: return '2';;
                case Keys.NumPad3: return '3';;
                case Keys.NumPad4: return '4';;
                case Keys.NumPad5: return '5';;
                case Keys.NumPad6: return '6';;
                case Keys.NumPad7: return '7';;
                case Keys.NumPad8: return '8';;
                case Keys.NumPad9: return '9';;

                //Special keys
                case Keys.OemTilde: if (shift) { return '~'; } else { return '`'; };
                case Keys.OemSemicolon: if (shift) { return ':'; } else { return ';'; };
                case Keys.OemQuotes: if (shift) { return '"'; } else { return '\''; };
                case Keys.OemQuestion: if (shift) { return '?'; } else { return '/'; };
                case Keys.OemPlus: if (shift) { return '+'; } else { return '='; };
                case Keys.OemPipe: if (shift) { return '|'; } else { return '\\'; };
                case Keys.OemPeriod: if (shift) { return '>'; } else { return '.'; };
                case Keys.OemOpenBrackets: if (shift) { return '{'; } else { return '['; };
                case Keys.OemCloseBrackets: if (shift) { return '}'; } else { return ']'; };
                case Keys.OemMinus: if (shift) { return '_'; } else { return '-'; };
                case Keys.OemComma: if (shift) { return '<'; } else { return ','; };
                case Keys.Space: return ' ';
                default : return '\0';
            }
        }

        public Vector2 ScreenPos
        {
            get { return screenPos; }
            set { screenPos = value; }
        }

        public int CursorPosition
        {
            get { return cursorPosition; }
            set { cursorPosition = value; }
        }

        public String Text
        {
            get { return text; }
            set { text = value; }
        }

        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }

        public bool Usable
        {
            get { return usable; }
            set { usable = value; }
        }
    }
}
