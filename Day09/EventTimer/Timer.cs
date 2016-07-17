using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace EventTimer
{
    public sealed class TimerEventData : EventArgs
    {
        private readonly string message;
        private readonly string info;
        public TimerEventData(string message, string info)
        {
            this.message = message;
            this.info = info;
        }

        public string Message { get { return message; } }
        public string Info { get { return info; } }
    }

    public class Timer
    {
        private System.Timers.Timer repeater;   
        public event EventHandler<TimerEventData> Events = delegate { };
        private TimerEventData eventData;
        /// <summary>
        /// Create Timer
        /// </summary>
        /// <param name="time">Time for a repeat</param>
        /// <param name="data">Event Data</param>
        /// <param name="isRepeat">Sets a value indicating event each time the interval elapses or only after the first time it elapses</param>
        public Timer(int time = 1000,TimerEventData data = null,bool isRepeat = true)
        {
            repeater = new System.Timers.Timer(time);
            repeater.AutoReset = isRepeat;
            repeater.Elapsed += OnTimedEvent;
            eventData = data;
        }
        /// <summary>
        /// Start repeater
        /// </summary>
        public void StartTimer()
        {
            repeater.Enabled = true;
        }
        /// <summary>
        /// Stop repeater
        /// </summary>
        public void StopTimer()
        {
            repeater.Enabled = false;
        }

        protected virtual void Sender(TimerEventData e)
        {
            EventHandler<TimerEventData> temp = Events;
            if (temp != null)
            {
                temp(this, e);
            }
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Sender(eventData);
        }
        /// <summary>
        /// Set EventData 
        /// </summary>
        /// <param name="e">EventData that will be sent</param>
        public void SetEventData(TimerEventData e)
        {
            eventData = e;
        }
    }
}
