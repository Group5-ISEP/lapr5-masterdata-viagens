namespace lapr5_masterdata_viagens.Shared
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T Value { get; private set; }
        public string Error { get; private set; }

        private Result(bool isSuccess, T value, string error)
        {
            this.IsSuccess = isSuccess;
            this.Value = value;
            this.Error = error;
        }

        public static Result<T> Ok(T value)
        {
            return new Result<T>(true, value, null);
        }

        public static Result<T> Fail(string error)
        {
            return new Result<T>(false, default(T), error);
        }

    }
}