using System;
using System.Collections.Generic;
using Com.Appsflyer;

namespace XamarinSample
{
    public class MyPurchaseValidationCallback
    : Java.Lang.Object, IAppsFlyerInAppPurchaseValidationCallback
    {
        void IAppsFlyerInAppPurchaseValidationCallback.OnInAppPurchaseValidationError(IDictionary<string, Java.Lang.Object> validationError)
        {
            // Handle Validation Error Logic
        }

        void IAppsFlyerInAppPurchaseValidationCallback.OnInAppPurchaseValidationFinished(IDictionary<string, Java.Lang.Object> validationResult)
        {
            // Handle Validation Finished Logic
        }
    }
}

