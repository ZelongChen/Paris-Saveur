using MicroMsg.sdk;
using System.Diagnostics;

namespace Paris_Saveur.Tools
{
    class WechatCallback : WXEntryBasePage
    {
        public override void OnSendMessageToWXResponse(SendMessageToWX.Resp response)
        {
            base.OnSendMessageToWXResponse(response);
            Debug.WriteLine(response);
        }

    }
}
