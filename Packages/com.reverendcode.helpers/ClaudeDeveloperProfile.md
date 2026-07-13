# Developer Profile

## Environment

- Unity (version varies per project) — do not assume a specific Unity version unless told
- JetBrains Rider — apply Rider conventions when writing C#
- Treat Unity as supporting up to .NET 9; .NET 10 support is partial and temperamental, so avoid .NET 10-exclusive features unless explicitly requested

---

## Naming & File Conventions

- Public members: PascalCase. Private fields: _camelCase (Rider/.NET Runtime + Unity conventions).
- File names use dot notation matching namespace: e.g. `Helpers.Timer.cs` — even when folder structure already mirrors namespacing, to aid Unity Editor search.

---

## Core Code Quality Principles

1. **Never do anything twice.** Every method/algorithm has a single point of truth; improvements propagate automatically.
2. **Design for generalization by default.** Assume you'll need to do something an infinite number of times. The abstraction ladder: special-case → parameterized → fully generalized. Performance optimization is implied but deprioritized unless necessary.

---

## Architecture & Design Patterns

- Back configs with interfaces (e.g. `ITetrisConfig`, `IDemonConfig`) so both ScriptableObjects and runtime structs can satisfy them. Justify when a concrete type is sufficient.
- Runtime state external systems should read but not write: `{ get; private set; }`. Justify when public field access is intentional (e.g. Inspector serialization).
- Singleton `Awake` pattern: null-check `Instance`, `Destroy(gameObject)` if duplicate, else assign `Instance` and call `DontDestroyOnLoad` where appropriate.
- Unity lifecycle methods (`Awake`, `Start`, `OnEnable`, `OnDisable`, etc.) are `private` or `protected` by default. Justify when `public` access is required.
- Any class/function not directly tied to the specific game/project belongs in the `Helpers` namespace (e.g. `Helpers.Timer`, `Helpers.RandomBag<T>`).

---

## Cross-System Communication

- **Same GameObject or unique descendant** (e.g. Monster > Model): `UnityEvent` / `UnityEvent<T>`.
- **Non-unique-descendant or unrelated** (e.g. Map > Hex, multiple instances): ScriptableObject Event Channel pattern.
- Justify tighter coupling when intentional.

---

## Error Handling — "Log Loud, Fail Soft"

- Don't crash unless it genuinely warrants it. Prefer surfacing errors via the Unity Console.
- `Debug.LogWarning` — uninitialized values, missing Inspector refs, `Awake`/`Start` setup issues, simple dev/scene mistakes.
- `Debug.LogError` — all other error types.

---

## AI-Generated Code Attribution

- Every class or method Claude writes/generates must be tagged with `[AiGenerated("Claude", "<current model>")]` using `Helpers.AiGeneratedAttribute` (`AllowMultiple = true`), where `<current model>` is substituted with Claude's actual model name at the time of generation (e.g. `"Sonnet 4.6"`).
- Do **not** use the built-in `[GeneratedCode]` — it triggers unwanted analyzer/coverage suppression.
- Tag at the class level if the whole class is generated; at the method level if only specific methods are added/changed.

---

## Memory Rule Meta-Protocol

- If a stored rule would produce an inferior technical outcome in a given case, flag it, explain why, propose revised wording, and update only after the developer confirms — showing the diff either way.
