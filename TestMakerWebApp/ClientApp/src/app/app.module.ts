import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

//Services
import { DataQuizService } from './services/data-quiz.service';
import { DataQuestionService } from './services/data-question.service';
import { DataAnswerService } from './services/data-answer.service';

//Components
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { QuizListComponent } from './quiz-list/quiz-list.component';
import { QuizComponent } from './quiz/quiz.component';
import { AboutComponent } from './about/about.component';
import { LoginComponent } from './login/login.component';
import { PageNotFoundComponent } from './pagenotfound/pagenotfound.component';
import { QuizEditComponent } from './quiz-edit/quiz-edit.component';
import { QuestionListComponent } from './question-list/question-list.component';
import { QuestionEditComponent } from './question-edit/question-edit.component';
import { AnswerEditComponent } from './answer-edit/answer-edit.component';
import { AnswerListComponent } from './answer-list/answer-list.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    QuizListComponent,
    QuizComponent,
    QuizEditComponent,
    AboutComponent,
    LoginComponent,
    PageNotFoundComponent,
    QuestionListComponent,
    QuestionEditComponent,
    AnswerEditComponent,
    AnswerListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      { path: 'quiz/create', component: QuizEditComponent },
      { path: 'quiz/edit/:id', component: QuizEditComponent },
      { path: 'quiz/:id', component: QuizComponent },
      { path: 'question/create/:id', component: QuestionEditComponent },
      { path: 'question/edit/:id', component: QuestionEditComponent },
      { path: 'answer/create/:id', component: AnswerEditComponent },
      { path: 'answer/edit/:id', component: AnswerEditComponent },
      { path: 'about', component: AboutComponent },
      { path: 'login', component: LoginComponent },
      { path: '**', component: PageNotFoundComponent }
    ])
  ],
  providers: [DataQuizService, DataQuestionService, DataAnswerService],
  bootstrap: [AppComponent]
})
export class AppModule { }
