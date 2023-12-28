# ThinkSharp.Licensing.Client

[![Build status](https://ci.appveyor.com/api/projects/status/l3aagqmbfmgxwv3t?svg=true)](https://ci.appveyor.com/project/JanDotNet/thinksharp-licensing)
[![NuGet](https://img.shields.io/nuget/v/ThinkSharp.Licensing.svg)](https://www.nuget.org/packages/ThinkSharp.Licensing/) [![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.TXT)
[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=MSBFDUU5UUQZL)

## Introduction

**ThinkSharp.Licensing** is a simple library with fluent API for creating and verifying signed licenses. It provides the following functionallities:

* verification of hardware identifiers (Windows only)
* verification of serial numbers
* verification of signed licenses

## API Reference

### Singed License

The class `SignedLicense` encapsulates some license related information and a signature for verifying it. The license can be serialized / deserialized for storing it on the client. It has following public properties:

* **IssueDate:** date of the issuing (when the license was created)
* **ExpirationDate:** date of the expiration (may be `DateTime.MaxValue` for licenses without expiration)
* **SerialNumber** Optional: a serial number (See class `SerialNumber` below)
* **Properties** `IDictionary<string, string>` with custom key value pairs

The static `Lic` class is the entry point for the fluent API that allows to work with signed licenses. It has the following static properties:

* **Lic.Verifyer:** Object for verifiying serialized signed licenses and deserialize it.

#### Usage

**Verify License**

For deserializing the license, the `Lic.Verifier` has to be used. If the license can not be deserialized hor has no valid signature, an exception is thrown.

```csharp
SignedLicense license = Lic.Verifier
			   .WithRsaPublicKey(publicKey)       // .WithSigner(ISigner)
		    	   .WithApplicationCode("ABC")        // .WithoutApplicationCode
		           .LoadAndVerify(licenseText);
```

### Hardware Identifier

The hardware identifier is an identifier that derives from 4 characteristics of the computer's hardware (processor ID, serial number of BIOS and so on). The identifier may look like:

    5BED5GAB-E5TGXKGK-01SI8MFF-7T099W78-SRH4

Each characteristic is encoded in one of first 4 parts (8 charachters). The hardware identifier will be accepted if at least 2 of the 4 characteristics are equal. That ensures, that the license doesn't become invalid if e.g. the processor of the computer changed. The last part (4 characters) is a check sum that can be used to detect errors in the the hardware identifier. 

#### Usage

```csharp
// Create:
string hardwareIdentifier = HardwareIdentifier.ForCurrentComputer();

// Validate Checksum
if (!HardwareIdentifier.IsCheckSumValid(hardwareIdentifier))
{
    Console.WriteLine("Entered hardware identifier has errors.");
}
        
// Validate for current computer
if (!HardwareIdentifier.IsValidForCurrentComputer(hardwareIdentifier))
{
    Console.WriteLine("Entered license is not valid for this computer.");
}
```
          
## License

ThinkSharp.Licensing is released under [The MIT license (MIT)](LICENSE.TXT)

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/JanDotNet/ThinkSharp.Licensing/tags). 
    
## Credits

Thanks to [Peter-B-](https://github.com/Peter-B-) for simplifying the project structure and improving compatibility to .Net 5.0.
    
## Donation
If you like ThinkSharp.Licensing and use it in your project(s), feel free to give me a cup of coffee :) 

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=MSBFDUU5UUQZL)
