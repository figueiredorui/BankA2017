namespace BankA.Data.Contexts
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using BankA.Data.Entities;
    using System.Data.Entity.Validation;
    using System.Collections.Generic;

    public partial class BankAContext
    {
        public override int SaveChanges()
        {
            try
            {
                var retVal = base.SaveChanges();
                return retVal;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = new List<string>();
                foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
                {
                    string entityName = validationResult.Entry.Entity.GetType().Name;
                    foreach (DbValidationError error in validationResult.ValidationErrors)
                    {
                        errorMessages.Add(entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                    }
                }

                throw new Exception(String.Join("\n", errorMessages.ToArray()));
            }
            catch (Exception)
            {
                throw;
            }
        }   
    }
}
