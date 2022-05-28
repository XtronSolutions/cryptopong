using System;

public static partial class Events
{
    public static Action<messageInfo> OnReportMessage = null;
    public static void DoReportMessage(messageInfo info) => OnReportMessage?.Invoke(info);
}

public struct messageInfo
{
    public bool Resend;
    public string message;
    public Action Callback;

    public messageInfo(string msg, Action callback = null, bool resend = false)
    {
        message = msg;
        Resend = resend;
        Callback = callback;
    }
}
