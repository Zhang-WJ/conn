namespace Contacts

open System

type Concat = {
    Id: Guid
    FirstName: string
    LastName: string
    Email: string
}

[<RequireQualifiedAccess>]
module Contact =
   let private isValidEmail (email:string) =
       try
           new System.Net.Mail.MailAddress(email) |> ignore
           true
       with
       | _ -> false
       
   let valid contact =
       let errors = seq {
           if(String.IsNullOrEmpty(contact.FirstName)) then
               yield "First name should not be empty"
           if(String.IsNullOrEmpty(contact.LastName)) then
               yield "Last name should not be empty"
           if(String.IsNullOrEmpty(contact.Email)) then
               yield "First name should not be empty"
           if((isValidEmail contact.Email) |>not) then
               yield "not a valid email"
       }
       
       if(Seq.isEmpty errors) then Ok contact else Error errors
       
   let createContact fname lname email =
       let c ={
           Id = Guid.NewGuid()
           FirstName = fname
           LastName = lname
           Email = email
       }
       valid c
