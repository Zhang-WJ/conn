namespace Contacts

open System

[<RequireQualifiedAccess>]
module Input =
       open System
       
       let private captureInput(label:string) =
           printf "%s" label
           Console.ReadLine()
          
       let private printErrors errs =
           printf "Errors"
           errs |> Seq.iter (printfn "%s")
           
       let rec private captureContact() =
           printf "ERRORS"
           Contact.createContact (captureInput "First name:")
                (captureInput "Last name:") (captureInput "Email:")
       
           |> fun r -> match r with
                        | Ok contact -> contact
                        | Error err ->
                            printErrors err
                            captureContact()
       let private captureContactChoice saveContact =
           let contact = captureContact()
           saveContact contact
           let another = captureInput "Continue (Y/N)?"
           match another.ToUpper() with
           | "Y" -> Choice1Of2 ()
           | _ ->  Choice2Of2 ()
           
       let rec private captureContacts saveContact =
           match captureContactChoice saveContact with
           | Choice1Of2 _ -> captureContacts saveContact
           | Choice2Of2 _ -> ()
           
       let printMenu() =
           printfn "=================="
           printfn "MENU"
           printfn "=================="
           printfn "1. Print Contacts"
           printfn "2. Capture Contacts"
           printfn "0. Quit"
           
       let routeMenuOption i getContacts saveContact =
           match i with
           | "1" ->
               printfn "Contacts"
               getContacts() |> List.iter (fun c -> printfn "%s %s (%s)" c.FirstName c.LastName c.Email)
           | "2" -> captureContacts saveContact
           | "0" -> printMenu()
           
       let readKey() =
           let k = Console.ReadKey()
           Console.WriteLine()
           k.KeyChar |> string