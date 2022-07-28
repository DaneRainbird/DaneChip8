using System.Windows.Forms;
using System.Windows.Input;

namespace DatenChip8.Core {
    public class Input {

        /// <summary>
        /// A dictionary of keys, with their associated input values. 
        /// Left is the allowed key inputs, right is their associated "Chip8 inputs" (i.e. pressing 4 correlates to pressing C)
        /// 1 | 2 | 3 | 4       |       1 | 2 | 3 | c
        /// q | w | e | r       |       4 | 5 | 6 | d
        /// a | s | d | f       |       7 | 8 | 9 | e
        /// z | x | c | v       |       a | 0 | b | f
        /// </summary>
        private Dictionary<Keys, int> keyMap = new Dictionary<Keys, int> {
            { Keys.D1, 1 },
            { Keys.D2, 2 },
            { Keys.D3, 3 },
            { Keys.D4, 12 },
            { Keys.Q, 4 },
            { Keys.W, 5 },
            { Keys.E, 6 },
            { Keys.R, 13 },
            { Keys.A, 7 },
            { Keys.S, 8 },
            { Keys.D, 9 },
            { Keys.F, 14 },
            { Keys.Z, 10 },
            { Keys.X, 0 },
            { Keys.C, 11 },
            { Keys.V, 15 }
        };

        // Array to store if keys have been pressed 
        private bool[] keysPressed = new bool[16];

        /// <summary>
        /// Sets the keysPressed array index of the key to be true if it is a valid key
        /// </summary>
        /// <param name="code">The key that was pressed.</param>
        public void setKeyPressed(Keys code) {
            if (keyMap.ContainsKey(code)) {
                keysPressed[keyMap[code]] = true;
            }
        }

        /// <summary>
        /// Sets the keysPressed array index of the key to be false if it is a valid key
        /// </summary>
        /// <param name="code">The key that was pressed.</param>
        public void setKeyReleased(Keys code) {
            if (keyMap.ContainsKey(code)) {
                keysPressed[keyMap[code]] = false;
            }
        }

        /// <summary>
        /// Returns if a key is currently pressed or not.
        /// </summary>
        /// <param name="index">The index of the key code to check for.</param>
        /// <returns>True if the key is pressed, false if not.</returns>
        public bool isKeyPressed(int index) {
            return keysPressed[index];
        }
    }
}
