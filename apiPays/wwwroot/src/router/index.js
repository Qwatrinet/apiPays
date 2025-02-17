import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import TripView from '@/views/TripView.vue'
import PersonView from '@/views/PersonView.vue'
import MoveView from '@/views/MoveView.vue'
import AddPersonView from '@/views/AddPersonForm.vue'
import ShowPeopleView from '@/views/ShowPeopleView.vue'
import AddTripView from '@/views/AddTripView.vue'
import ShowTripView from '@/views/ShowTripView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/trip',
      name: 'trip',
      component: TripView
    },
    {
      path: '/person',
      name: 'person',
      component: PersonView
    },
    {
      path: '/move',
      name: 'move',
      component: MoveView
    },
    {
      path: '/person/add',
      name: 'addPerson',
      component: AddPersonView
    },
    {
      path: '/person/show',
      name: 'showPeople',
      component: ShowPeopleView
    },
    {
      path: '/trip/add',
      name: 'addTrip',
      component: AddTripView
    },
    {
      path: '/trip/show',
      name: 'showTrips',
      component: ShowTripView
    },

  ]
})

export default router
