using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Media.Imaging; 

/// <summary>
/// 
/// /Weapons/WeaponLoader.cs
/// 
/// Created: 2019-12-05
/// 
/// Modified: 2019-12-05
/// 
/// Version: 1.00
/// 
/// Purpose: Loads weapons from Weapons.xml.
/// 
/// </summary>

namespace Free
{
    partial class FreeSDL
    {
        public void LoadWeapons() //TODO: XMLManager
        {
            try
            {
                XmlDocument XmlDocument = new XmlDocument();
                XmlDocument.Load("Weapons.xml");
                XmlNode XmlRootNode = XmlDocument.FirstChild;

                while (XmlRootNode.Name != "Weapons")
                {
                    XmlRootNode = XmlRootNode.NextSibling; // ignore all other nodes. TODO - check what it triggers when we run out of nodes, so we can catch the exception.
                }

                XmlNodeList XmlNodes = XmlRootNode.ChildNodes; // get the children of the IGameObjects node.

                foreach (XmlNode XmlNode in XmlNodes)
                {
                    XmlAttributeCollection XmlAttributes = XmlNode.Attributes;

                    if (XmlAttributes.Count > 0) // prevent crashing if no attributes.
                    {
                        Weapon Weapon = new Weapon(); // CREATE THE WEAPON TYPE.
                        foreach (XmlAttribute XmlAttribute in XmlAttributes)
                        {
                            switch (XmlAttribute.Name) // loop thru the names
                            {
                                case "Accuracy":
                                case "accuracy": // accuracy
                                    Weapon.WEAPONACCURACY = Convert.ToDouble(XmlAttribute.Value);
                                    continue;
                                case "Ammunition":
                                case "ammunition":
                                case "ammo": // ammo
                                    Weapon.WEAPONAMMO = Convert.ToDouble(XmlAttribute.Value);
                                    continue;
                                case "CriticalHitChance":
                                case "criticalhitchance":
                                case "criticalchance":
                                case "critchance": // critical hit chance
                                    Weapon.WEAPONCRITICALHITCHANCE = Convert.ToDouble(XmlAttribute.Value);
                                    continue;
                                case "CriticalHitMultiplier":
                                case "criticalhitmultiplier":
                                case "criticalmultiplier":
                                case "critmultiplier":
                                    Weapon.WEAPONCRITICALHITDAMAGEMULTIPLIER = Convert.ToDouble(XmlAttribute.Value);
                                    continue;
                                case "CriticalHitDamageUncertainty":
                                case "CriticalHitUncertainty":
                                case "criticalhituncertainty":
                                case "criticaluncertainty":
                                case "crituncertainty": // crit uncertainty
                                    Weapon.WEAPONCRITICALHITDAMAGEUNCERTAINTY = Convert.ToDouble(XmlAttribute.Value);
                                    continue;
                                case "Damage":
                                case "damage":
                                case "dmg": // damage
                                    Weapon.WEAPONDAMAGE = Convert.ToDouble(XmlAttribute.Value);
                                    continue;
                                case "DamageUncertainty":
                                case "damageuncertainty":
                                case "dmguncertainty": // damage uncertainity
                                    Weapon.WEAPONDAMAGEUNCERTAINTY = Convert.ToDouble(XmlAttribute.Value);
                                    continue;
                                case "WeaponFireRate":
                                case "weaponfirerate":
                                case "firerate": // fire rate
                                    Weapon.WEAPONFIRERATE = Convert.ToDouble(XmlAttribute.Value);
                                    continue;
                                case "WeaponImage":
                                case "weaponimage":
                                case "weaponimg":
                                case "image":
                                case "img": // weapon image
                                case "Image":
                                    Weapon.WEAPONIMAGE = new WriteableBitmap(BitmapFactory.FromStream(new FileStream(XmlAttribute.Value, FileMode.Open)));

                                    continue;
                                case "WeaponAmmoImage":
                                case "weaponammunitionimage":
                                case "weaponammunitionimg":
                                case "weaponammoimage":
                                case "weaponammoimg":
                                case "ammunitionimage":
                                case "ammunitionimg":
                                case "ammoimage":
                                case "ammoimg":
                                case "imageammo":
                                case "imgammo":
                                case "imageammunition":
                                case "imgammunition": //ammo
                                case "WeaponAmmunitionImage":
                                    Weapon.WEAPONIMAGEAMMO = new WriteableBitmap(BitmapFactory.FromStream(new FileStream(XmlAttribute.Value, FileMode.Open)));
                                    continue;
                                case "name": //name
                                    Weapon.WEAPONNAME = XmlAttribute.Value;
                                    continue;
                            }
                        }
                        WeaponList.Add(Weapon);
                    }
                }
            }
            catch (XmlException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "An error occurred loading Weapons.xml.", "avant-gardé engine Ver2.13", 27);
                return;
            }
            catch (FormatException err)
            {
                Error.Throw(err, ErrorSeverity.FatalError, "An invalid weapon entry was located within Weapons.xml.", "avant-gardé engine Ver2.13", 28 );
            }
        }
    }
}
