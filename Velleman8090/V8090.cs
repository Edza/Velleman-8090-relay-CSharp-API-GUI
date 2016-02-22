using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velleman.Kits;

namespace Velleman8090
{  
    public class V8090
    {
        private static V8090 instance;

        K8090Board _board;
        BitArray _subrelays;

        private V8090()
        {
            this._board = new K8090Board(COM.GetPort());
            this._subrelays = new BitArray(8);
            this.Reset();
        }

        private void Send(Action todo)
        {
            this._board.Connect();
            todo();
            this._board.Disconnect();
        }

        private void Reset()
        {
            this.Send(() => this._board.SwitchRelayOff(0xFF)); // switch off all
        }

        private void Validate(int relay)
        {
            if (relay < 0 || relay > 7)
                throw new NotSupportedException();
        }

        private byte BitArrayToByte(BitArray bitArray)
        {
            if (bitArray.Count != 8)
            {
                throw new ArgumentException("Bit array is not a byte");
            }
            byte[] bytes = new byte[1];
            bitArray.CopyTo(bytes, 0);
            return bytes[0];
        }

        public static V8090 Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new V8090();
                }
                return instance;
            }
        }

        /// <summary>
        /// Gets subrelays state
        /// </summary>
        /// <param name="index">From 0 to 7</param>
        /// <returns>On or off</returns>
        public bool Get(int index)
        {
            this.Validate(index);
            return this._subrelays[index];
        }

        /// <summary>
        /// Turns subrelay on
        /// </summary>
        /// <param name="index">From 0 to 7</param>
        public void On(int index)
        {
            this.Validate(index);
            BitArray on = new BitArray(8);
            on.Set(index, true);
            this.Send(() => this._board.SwitchRelayOn(this.BitArrayToByte(on)));
            this._subrelays.Set(index, true);
        }

        /// <summary>
        /// Turns subrelay off
        /// </summary>
        /// <param name="index">From 0 to 7</param>
        public void Off(int index)
        {
            this.Validate(index);
            BitArray off = new BitArray(8);
            off.Set(index, true);
            this.Send(() => this._board.SwitchRelayOff(this.BitArrayToByte(off)));
            this._subrelays.Set(index, false);
        }

        /// <summary>
        /// Toggles subrelay
        /// </summary>
        /// <param name="index">From 0 to 7</param>
        public void Toggle(int index)
        {
            this.Validate(index);
            BitArray toggle = new BitArray(8);
            toggle.Set(index, true);
            this.Send(() => this._board.ToggleRelay(this.BitArrayToByte(toggle)));
            this._subrelays.Set(index, !this._subrelays.Get(index));
        }
    }
}
