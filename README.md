# ActionFilterValidator

A clean and extensible **ASP.NET Core Web API** demonstrating how to use **FluentValidation** together with a **custom Action Filter** for centralized and reusable request validation.

This project eliminates repetitive validation logic from controllers by automatically handling validation before an action executes — resulting in cleaner and more maintainable APIs.

---

## 🚀 Features

- ✅ Centralized request validation using a custom **Action Filter**
- ✅ Automatic model validation via **FluentValidation**
- ✅ Consistent and structured validation error responses
- ✅ Plug-and-play setup — add your own validators, and it just works
- ✅ Example DTOs and Validators included

---

## 🧠 How It Works

Instead of manually validating models in each controller like:

```csharp
if (!ModelState.IsValid)
    return BadRequest(ModelState);
```
This project registers a global **Action Filter (`ValidationFilter`)** that:

1. Detects if the request model has a corresponding FluentValidation validator
2. Validates the model before the controller action executes
3. Returns a consistent JSON error response if validation fails

### Example Response

```json
{
  "success": false,
  "message": "Validation failed",
  "errors": {
    "Name": ["Name must not be empty."],
    "Age": ["Age must be greater than 10."]
  }
}
```

## 🧱 How to Add Your Own Validators

You can easily define new validation rules for your DTOs.

### Example

#### Create a DTO

```csharp
public class CreateProductDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
```

#### Create a Validator

```csharp
using FluentValidation;

public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(p => p.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");
    }
}
```

✅ **Done**  
No need to modify controllers! As long as your validator inherits from `AbstractValidator<T>` and is registered in DI, the `ValidationFilter` will automatically pick it up.

## 🧰 Tech Stack

- ASP.NET Core 9.0
- FluentValidation
- Action Filters
- C# 12
- Swagger / OpenAPI
