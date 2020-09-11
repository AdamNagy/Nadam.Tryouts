namespace DataEntity
{
    public interface IJsonStringEntity
    {
        /// <summary>
        /// Reads the given property value from json file if exist, reads the whole file otherwise
        /// </summary>
        /// <param name="propertyName"> name of the proprtx to look for - optional </param>
        /// <returns></returns>
        string Read(string propertyName = "");

        /// <summary>
        /// set a property on the json string in the file
        /// </summary>
        /// <param name="propertyName">the name of the property, it will be looked for first and if exist it changes its value, if not adds it</param>
        /// <param name="newValue"> the new value of property as string </param>
        /// <param name="appendTo"> if property does not exist, it will be append, this param tells where </param>
        void SetProperty(
            string propertyName,
            string newValue);

        /// <summary>
        /// Extends an existing property in the json. Value can be array or object, NOT primitive
        /// </summary>
        /// <param name="newValue"> new value as string, need to be json formatted </param>
        /// <param name="arrayPropertyName"> property name to look for </param>
        /// <param name="appendTo"> place can be specified: new value van be added to the begining or to the end </param>
        void ExtendProperty(
            string newValue,
            string arrayPropertyName,
            AppendPosition appendTo = AppendPosition.begining);

        /// <summary>
        /// Removes a value from a property. Property type must be object(complex) or array
        /// </summary>
        /// <param name="value"> the value to look for - need to be json formatted</param>
        /// <param name="arrayPropertyName"></param>
        /// <returns></returns>
        string ReduceProperty(
            string value,
            string arrayPropertyName = "");
    }
}
