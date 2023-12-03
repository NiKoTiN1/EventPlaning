import { FieldType } from "./field-type.enum";

export interface EventField {
    id: string,
    name: string,
    description: string,
    fieldType: FieldType
}
