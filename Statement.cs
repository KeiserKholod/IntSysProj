namespace ProcessingTextFile
{
    public class Statement
    {
        public string StatementA;
        public string StatementB;

        public string OperandType;

        /// <summary>
        /// Возвращает строковое представление условия.
        /// </summary>  
        public string StringRepresentation
        {
            get => StatementA + OperandType + StatementB;
        }
    }
}
