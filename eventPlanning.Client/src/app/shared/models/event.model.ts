import { EventField } from "./event-field.model";

export interface EventModel {
    id: string,
    eventName: string,
    eventDescription: string,
    startDate: Date,
    maxCapacity: number,
    eventFields: Array<EventField>,
}
