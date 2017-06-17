using System.Windows.Input;

namespace asterTake2
{
    public class InputHandler
    {
        public UserInput InputReader()
        {
            var userInput = new UserInput
            {
                IsUpKeyPressed = Keyboard.IsKeyDown(Key.Up),
                IsLeftKeyPressed = Keyboard.IsKeyDown(Key.Left),
                IsRightKeyPressed = Keyboard.IsKeyDown(Key.Right),
                IsDownKeyPressed = Keyboard.IsKeyDown(Key.Down),
                IsShooting = Keyboard.IsKeyDown(Key.LeftCtrl),
                UserRequestedToExit = Keyboard.IsKeyDown(Key.Escape),
                UserRequestedDebug = Keyboard.IsKeyDown(Key.D),
                UserRequestedConsole = Keyboard.IsKeyDown(Key.X)
            };
            return userInput;
        }
    }

    public class UserInput
    {
        public bool IsUpKeyPressed { get; set; }
        public bool IsLeftKeyPressed { get; set; }
        public bool IsRightKeyPressed { get; set; }
        public bool IsDownKeyPressed { get; set; }
        public bool IsShooting { get; set; }
        public bool UserRequestedToExit { get; set; }
        public bool UserRequestedDebug { get; set; }
        public bool UserRequestedConsole { get; set; }
    }
}