/*
 * Copyright (c) 2014-2025 GraphDefined GmbH <achim.friedland@graphdefined.com>
 * This file is part of WWCP DatexII <https://github.com/OpenChargingCloud/WWCP_DatexII>
 *
 * Licensed under the Affero GPL license, Version 3.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.gnu.org/licenses/agpl.html
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.Common
{

    /// <summary>
    /// Extension methods for InformationDeliveryServices.
    /// </summary>
    public static class InformationDeliveryServiceExtensions
    {

        /// <summary>
        /// Indicates whether this InformationDeliveryService is null or empty.
        /// </summary>
        /// <param name="InformationDeliveryService">An InformationDeliveryService.</param>
        public static Boolean IsNullOrEmpty(this InformationDeliveryService? InformationDeliveryService)
            => !InformationDeliveryService.HasValue || InformationDeliveryService.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this InformationDeliveryService is null or empty.
        /// </summary>
        /// <param name="InformationDeliveryService">An InformationDeliveryService.</param>
        public static Boolean IsNotNullOrEmpty(this InformationDeliveryService? InformationDeliveryService)
            => InformationDeliveryService.HasValue && InformationDeliveryService.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An InformationDeliveryService.
    /// </summary>
    public readonly struct InformationDeliveryService : IId,
                                                        IEquatable<InformationDeliveryService>,
                                                        IComparable<InformationDeliveryService>
    {

        #region Data

        private readonly static Dictionary<String, InformationDeliveryService>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                                          InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this InformationDeliveryService is null or empty.
        /// </summary>
        public readonly  Boolean                          IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this InformationDeliveryService is NOT null or empty.
        /// </summary>
        public readonly  Boolean                          IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the InformationDeliveryService.
        /// </summary>
        public readonly  UInt64                           Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered InformationDeliveryServices.
        /// </summary>
        public static    IEnumerable<InformationDeliveryService>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new InformationDeliveryService based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an InformationDeliveryService.</param>
        private InformationDeliveryService(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static InformationDeliveryService Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new InformationDeliveryService(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an InformationDeliveryService.
        /// </summary>
        /// <param name="Text">A text representation of an InformationDeliveryService.</param>
        public static InformationDeliveryService Parse(String Text)
        {

            if (TryParse(Text, out var informationDeliveryService))
                return informationDeliveryService;

            throw new ArgumentException($"Invalid text representation of an InformationDeliveryService: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an InformationDeliveryService.
        /// </summary>
        /// <param name="Text">A text representation of an InformationDeliveryService.</param>
        public static InformationDeliveryService? TryParse(String Text)
        {

            if (TryParse(Text, out var informationDeliveryService))
                return informationDeliveryService;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out InformationDeliveryService)

        /// <summary>
        /// Try to parse the given text as an InformationDeliveryService.
        /// </summary>
        /// <param name="Text">A text representation of an InformationDeliveryService.</param>
        /// <param name="InformationDeliveryService">The parsed InformationDeliveryService.</param>
        public static Boolean TryParse(String Text, out InformationDeliveryService InformationDeliveryService)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out InformationDeliveryService))
                    InformationDeliveryService = Register(Text);

                return true;

            }

            InformationDeliveryService = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this InformationDeliveryService.
        /// </summary>
        public InformationDeliveryService Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// Includes any general delivery channel such as broadcast channels (e.g. radio,
        /// tv, RDS-TMC, TPEG services, etc.) or web publishing available to public or
        /// to specific users, depending on Service Provider policies.
        /// </summary>
        public static InformationDeliveryService  AnyGeneralDeliveryService    { get; }
            = Register("anyGeneralDeliveryService");

        /// <summary>
        /// Specific services which deliver warning alerts to end users to enhance safety
        /// via any specific application available to drivers, including C-ITS services.
        /// </summary>
        public static InformationDeliveryService  SafetyServices               { get; }
            = Register("safetyServices");

        /// <summary>
        /// Variable Message Signs or any other visual roadside devices which information
        /// are accessible to drivers which aim to affect driving style improving safety
        /// and road network LoS.
        /// </summary>
        public static InformationDeliveryService  Vms                          { get; }
            = Register("vms");

        #endregion


        #region Operator overloading

        #region Operator == (InformationDeliveryService1, InformationDeliveryService2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="InformationDeliveryService1">An InformationDeliveryService.</param>
        /// <param name="InformationDeliveryService2">Another InformationDeliveryService.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (InformationDeliveryService InformationDeliveryService1,
                                           InformationDeliveryService InformationDeliveryService2)

            => InformationDeliveryService1.Equals(InformationDeliveryService2);

        #endregion

        #region Operator != (InformationDeliveryService1, InformationDeliveryService2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="InformationDeliveryService1">An InformationDeliveryService.</param>
        /// <param name="InformationDeliveryService2">Another InformationDeliveryService.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (InformationDeliveryService InformationDeliveryService1,
                                           InformationDeliveryService InformationDeliveryService2)

            => !InformationDeliveryService1.Equals(InformationDeliveryService2);

        #endregion

        #region Operator <  (InformationDeliveryService1, InformationDeliveryService2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="InformationDeliveryService1">An InformationDeliveryService.</param>
        /// <param name="InformationDeliveryService2">Another InformationDeliveryService.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (InformationDeliveryService InformationDeliveryService1,
                                          InformationDeliveryService InformationDeliveryService2)

            => InformationDeliveryService1.CompareTo(InformationDeliveryService2) < 0;

        #endregion

        #region Operator <= (InformationDeliveryService1, InformationDeliveryService2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="InformationDeliveryService1">An InformationDeliveryService.</param>
        /// <param name="InformationDeliveryService2">Another InformationDeliveryService.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (InformationDeliveryService InformationDeliveryService1,
                                           InformationDeliveryService InformationDeliveryService2)

            => InformationDeliveryService1.CompareTo(InformationDeliveryService2) <= 0;

        #endregion

        #region Operator >  (InformationDeliveryService1, InformationDeliveryService2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="InformationDeliveryService1">An InformationDeliveryService.</param>
        /// <param name="InformationDeliveryService2">Another InformationDeliveryService.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (InformationDeliveryService InformationDeliveryService1,
                                          InformationDeliveryService InformationDeliveryService2)

            => InformationDeliveryService1.CompareTo(InformationDeliveryService2) > 0;

        #endregion

        #region Operator >= (InformationDeliveryService1, InformationDeliveryService2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="InformationDeliveryService1">An InformationDeliveryService.</param>
        /// <param name="InformationDeliveryService2">Another InformationDeliveryService.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (InformationDeliveryService InformationDeliveryService1,
                                           InformationDeliveryService InformationDeliveryService2)

            => InformationDeliveryService1.CompareTo(InformationDeliveryService2) >= 0;

        #endregion

        #endregion

        #region IComparable<InformationDeliveryService> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two InformationDeliveryServices.
        /// </summary>
        /// <param name="Object">An InformationDeliveryService to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is InformationDeliveryService informationDeliveryService
                   ? CompareTo(informationDeliveryService)
                   : throw new ArgumentException("The given object is not an InformationDeliveryService!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(InformationDeliveryService)

        /// <summary>
        /// Compares two InformationDeliveryServices.
        /// </summary>
        /// <param name="InformationDeliveryService">An InformationDeliveryService to compare with.</param>
        public Int32 CompareTo(InformationDeliveryService InformationDeliveryService)

            => String.Compare(InternalId,
                              InformationDeliveryService.InternalId,
                              StringComparison.Ordinal);

        #endregion

        #endregion

        #region IEquatable<InformationDeliveryService> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two InformationDeliveryServices for equality.
        /// </summary>
        /// <param name="Object">An InformationDeliveryService to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is InformationDeliveryService informationDeliveryService &&
                   Equals(informationDeliveryService);

        #endregion

        #region Equals(InformationDeliveryService)

        /// <summary>
        /// Compares two InformationDeliveryServices for equality.
        /// </summary>
        /// <param name="InformationDeliveryService">An InformationDeliveryService to compare with.</param>
        public Boolean Equals(InformationDeliveryService InformationDeliveryService)

            => String.Equals(InternalId,
                             InformationDeliveryService.InternalId,
                             StringComparison.Ordinal);

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        public override Int32 GetHashCode()

            => InternalId?.ToLower().GetHashCode() ?? 0;

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this object.
        /// </summary>
        public override String ToString()

            => InternalId ?? "";

        #endregion

    }

}
