using MvcPL.Models.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace MvcPL.Models.Attributes
{
    public class ValidateFileAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            byte[] result = null;
            const int mByte = 1024;
            int maxContent = mByte * mByte; 
            var file = value as HttpPostedFileBase;
            try
            {
                using (var img = Image.FromStream(file.InputStream))
                {
                    result = ImageHelper.ImageToByteArray(img);
                }
            }
            catch (ArgumentException)
            {
                result = null;
            }  
            if ((file == null) || (result == null))
            {
                ErrorMessage = "Your picture is wrong";
                return false;
            }              
            else if (file.ContentLength > maxContent)
            {
                ErrorMessage = "Your picture is too large, maximum allowed size is : " + (maxContent / mByte).ToString() + "MB";
                return false;
            }
            else
                return true;
        }
    }
}