namespace UniSQLite.Mappers
{
    public abstract class Mapper
    {
        public abstract object GetObject(string text);
        public abstract string GetString(object obj);
    }

    public abstract class Mapper<T> : Mapper
    {
        protected abstract T Deserialize(string text);
        protected abstract string Serialize(T obj);

        public override object GetObject(string text)
        {
            return Deserialize(text);
        }

        public override string GetString(object obj)
        {
            return Serialize((T)obj);
        }
    }
}