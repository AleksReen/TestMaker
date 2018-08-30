import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

//Services
import { DataQuizService } from './services/data-quiz.service';
import { DataQuestionService } from './services/data-question.service';
import { DataAnswerService } from './services/data-answer.service';
import { DataResultService } from './services/data-result.service';
import { AuthService } from './services/auth.service';
import { UserService } from './services/user.service';

//Interceptors
import { AuthInterceptor } from './services/auth-interceptor';
import { AuthResponseInterceptor } from './services/auth-response-interceptor';

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
import { ResultListComponent } from './result-list/result-list.component';
import { ResultEditComponent } from './result-edit/result-edit.component';
import { QuizSearchComponent } from './quiz-search/quiz-search.component';
import { RegisterComponent } from './register/register.component';

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
    AnswerListComponent,
    ResultListComponent,
    ResultEditComponent,
    QuizSearchComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
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
      { path: 'result/create/:id', component: ResultEditComponent },
      { path: 'result/edit/:id', component: ResultEditComponent },
      { path: 'about', component: AboutComponent },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: '**', component: PageNotFoundComponent }
    ])
  ],
  providers: [
    DataQuizService,
    DataQuestionService,
    DataAnswerService,
    DataResultService,
    AuthService,
    UserService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi:true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthResponseInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
