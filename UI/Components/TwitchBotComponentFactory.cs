using LiveSplit.Model;
using System;

namespace LiveSplit.UI.Components
{
    public class TwitchBotComponentFactory : IComponentFactory
    {
        public string ComponentName => "TwitchBot";

        public string Description => "A Twitch bot inside LiveSplit";

        public ComponentCategory Category => ComponentCategory.Other;

        public IComponent Create(LiveSplitState state) => new CounterComponent(state);

        public string UpdateName => ComponentName;

        public string XMLURL => "http://livesplit.org/update/Components/update.LiveSplit.TwitchBot.xml";

        public string UpdateURL => "http://livesplit.org/update/";

        public Version Version => Version.Parse("1.8.0");
    }
}
