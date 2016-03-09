using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlameWars
{
    class Global
    {
        SoundManager sm = new SoundManager(0,0);
        OptionsManager om = new OptionsManager();
        StateManager stm = new StateManager();
        ArtManager am = new ArtManager();
        GameManager gm = new GameManager();
    }
}
