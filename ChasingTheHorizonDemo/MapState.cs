using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class MapState : CursorState
    {
        public MapState(CursorController cursor) : base(cursor)
        {
        }

        public override IEnumerator MapCursor()
        {
            yield break;
        }
    }
}
