import { Teacher } from './teacher.model';

export interface Course {
  course: {
    courseCode: string;
    courseTitle: string;
    credits: number;
    courseType: string | null;
    objective: string | null;
    teachingMethods: string | null;
    content: string | null;
    learningMaterial: string | null;
    locationType: string | null;
    qualifications: string | null;
    employerConnections: string | null;
    examSchedule: string | null;
    internationalConnections: string | null;
    workload: string | null;
    contentScheduling: string | null;
    courseInformation: string | null;
    furtherInformation: string | null;
    evaluationScale: string | null;
    facultyName: string | null;
    onlineCredits: number | null;
    contactCredits: number | null;
    minSeats: number | null;
    maxSeats: number | null;
    reviews: string | null; // TODO: Change to Review[]
  };
  teachers: Teacher[];
}
