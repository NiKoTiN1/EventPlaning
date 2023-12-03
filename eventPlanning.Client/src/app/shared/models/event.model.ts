import { EventField } from "./event-field.model";
import { PageMode } from "./page-mode.enum";

export interface EventModel {
    id: string,
    eventName: string,
    eventDescription: string,
    startDate: Date,
    maxCapacity: number,
    eventFields: Array<EventField>,
}
