import { EventField } from "./event-field.model";

export interface CreateEvent {
    eventName: string;
    eventDescription: string,
    startDate: Date,
    maxCapacity: number,
    eventFields: Array<EventField>
}
