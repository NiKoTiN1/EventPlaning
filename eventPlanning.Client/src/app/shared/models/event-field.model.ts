import { FieldType } from "./field-type.enum";

export interface EventField {
    name: string,
    description: string,
    type: FieldType
}
