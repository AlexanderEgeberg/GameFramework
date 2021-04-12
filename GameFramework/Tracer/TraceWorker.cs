using System.Diagnostics;
using System.IO;

namespace GameFramework.Tracer
{
    public static class TraceWorker
    {

        static TraceSource ts = new TraceSource("Name");

        static TraceWorker()
        {

            ts.Switch = new SourceSwitch("Name","All");
            //TODO better path ??
            TraceListener fileLog = new TextWriterTraceListener(new StreamWriter(@"..\..\..\..\GameLog.txt"));
            ts.Listeners.Add(fileLog);




        }

        public static void Write(TraceEventType eventType,int id,string message)
        {
            ts.TraceEvent(eventType,id, message);

            ts.Flush();
        }
    }
}
