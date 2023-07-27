using CsvHelper;
using CsvHelper.Configuration;
using NLog;
using Pair_of_employees_who_have_worked_together.Constants;
using Pair_of_employees_who_have_worked_together.Helpers;
using Pair_of_employees_who_have_worked_together.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Pair_of_employees_who_have_worked_together.Services
{
    public class CSVReaderService
    {
        #region Declarations

        private readonly string _path;

        #endregion

        #region Initialize

        public CSVReaderService(string path)
        {
            this._path = path;
        }

        #endregion

        #region Methods

        public IEnumerable<EmployeeRowDTO> ReadCSVData()
        {
            IEnumerable<EmployeeRowDTO> result = new List<EmployeeRowDTO>();
            try
            {
                using (var reader = new StreamReader(this._path))
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = false,
                    };
                    using (var csv = new CsvReader(reader, config))
                    {
                        csv.Context.RegisterClassMap<EmployeeRowDTOMapper>();
                        result = csv.GetRecords<EmployeeRowDTO>().ToList();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger log = LogManager.GetCurrentClassLogger();
                log.Error(ApplicationConstants.ErrorParsing, ex.Message);
                return result;
            }
        }

        #endregion
    }
}
