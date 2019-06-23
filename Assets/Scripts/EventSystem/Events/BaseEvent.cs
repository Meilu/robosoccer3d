namespace EventSystem.Events
{
    public class BaseEvent : Event
    {
        public override string ToString(){
            return $"{base.ToString()}";
        }
    }
}