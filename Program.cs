namespace SqlLightest
{
    internal class Program
    {
        // SCOPE
        // 1.Syntax/statement validator
        // 2.INSERT
        // 3.SELECT (conditions and aggregates)
        // 4.UPDATE
        // 5.DELETE
        // 6.ALTER
        // 7.INDEX
        // 8.JOINS
        // 9.BINARY FILE
        public static void Main(string[] args)
        {
            var ioController = new IOController();
            ioController.HandleInputOutput();
        }
    }
}
