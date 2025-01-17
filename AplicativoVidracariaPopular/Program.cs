using GemBox.Pdf;

namespace AplicativoVidracariaPopular
{
    internal static class Program
    {
        public static double valorVidro = 0.02;
        public static double valorVidroAR = 0.06;
        public static double valorMdf = 0.01;

        public static bool[] moldura1 = { false, false, false, false, false, false, false, false, false, false };
        public static bool[] moldura2 = { false, false, false, false, false, false, false, false, false, false };
        public static bool[] moldura3 = { false, false, false, false, false, false, false, false, false, false };
        public static bool[] vidro = { false, false, false, false, false, false, false, false, false, false };
        public static bool[] vidroAR = { false, false, false, false, false, false, false, false, false, false };
        public static bool[] mdf = { false, false, false, false, false, false, false, false, false, false };
        public static bool[] paspatur = { false, false, false, false, false, false, false, false, false, false };
        public static bool[] extra = { false, false, false, false, false, false, false, false, false, false };
        public static bool[] quadroExistente = { false, false, false, false, false, false, false, false, false, false };
        public static string[] moldura1Tipo1 = { "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000" };
        public static string[] moldura1Tipo2 = { "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000" };
        public static string[] moldura2Tipo1 = { "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000" };
        public static string[] moldura2Tipo2 = { "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000" };
        public static string[] moldura3Tipo1 = { "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000" };
        public static string[] moldura3Tipo2 = { "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000" };
        public static double[] molduraTamanho1 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static double[] molduraTamanho2 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static double[] molduraExtra = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static double[] quantidade = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static double[] paspaturTamanho = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static double[] paspaturValor = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static double[] extraValor = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static string[] descricao = { "", "", "", "", "", "", "", "", "", "" };
        public static double[] valorUnitario = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static double[] valorTotal = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static double valorTotalGeral = 0;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ComponentInfo.SetLicense("FREE-LIMITED-KEY"); //GemBox

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}