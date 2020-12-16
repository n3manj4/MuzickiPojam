import { AnswerViewModel } from './answer-view-model';
import { GroupViewModel } from './signal-models/signal-view-model';

export class GameViewModel{
    id: string
    term: string
    answers: AnswerViewModel[]
    room: GroupViewModel
}
