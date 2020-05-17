using LiveSplit.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using LiveSplit.Model.Input;
using LiveSplit.Web.Share;
using System.Threading;

namespace LiveSplit.UI.Components
{
    public class CounterComponent : IComponent
    {
        public CounterComponent(LiveSplitState state)
        {
            Settings = new CounterComponentSettings();
            this.state = state;

            Start();
            SendMessage("coucou");
        }

        public void Start()
        {
            if (!Twitch.Instance.IsLoggedIn)
            {
                var thread = new Thread(() => Twitch.Instance.VerifyLogin()) { ApartmentState = ApartmentState.STA };
                thread.Start();
                thread.Join();
            }

            if (!Twitch.Instance.ConnectedChats.ContainsKey(Twitch.Instance.ChannelName))
            {
                Twitch.Instance.ConnectToChat(Twitch.Instance.ChannelName);
            }

            Twitch.Instance.Chat.OnMessage += OnMessage;
        }

        private void SendMessage(string message)
        {
            Twitch.Instance.Chat.SendMessage("/me " + message);
        }

        private void OnMessage(object sender, TwitchChat.Message message)
        {
            SendMessage("ok");
        }

        public CounterComponentSettings Settings { get; set; }

        public GraphicsCache Cache { get; set; }

        public float VerticalHeight { get; set; }

        public float MinimumHeight { get; set; }

        public float MinimumWidth { get;}

        public float HorizontalWidth { get; set; }

        public IDictionary<string, Action> ContextMenuControls
        {
            get { return null; }
        }

        public float PaddingTop { get; set; }
        public float PaddingLeft { get; }
        public float PaddingBottom { get; set; }
        public float PaddingRight { get; }

        protected SimpleLabel CounterNameLabel = new SimpleLabel();
        protected SimpleLabel CounterValueLabel = new SimpleLabel();

        protected Font CounterFont { get; set; }

        private LiveSplitState state;

        public void DrawHorizontal(Graphics g, Model.LiveSplitState state, float height, Region clipRegion) {}

        public void DrawVertical(System.Drawing.Graphics g, Model.LiveSplitState state, float width, Region clipRegion) {}

        public string ComponentName
        {
            get { return "TwitchBot"; }
        }

        public Control GetSettingsControl(LayoutMode mode)
        {
            return Settings;
        }

        public System.Xml.XmlNode GetSettings(System.Xml.XmlDocument document)
        {
            return Settings.GetSettings(document);
        }

        public void SetSettings(System.Xml.XmlNode settings)
        {
            Settings.SetSettings(settings);
        }

        public void Update(IInvalidator invalidator, Model.LiveSplitState state, float width, float height, LayoutMode mode) {}

        public void Dispose() {}
    }
}
