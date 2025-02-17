<script>
import TripVue from '@/components/TripVue.vue';
import DataManagement from '@/classes/DataManagement';
export default {
    data() {
        return {
            trips: []
        }
    },
    components: {
        TripVue
    },
    async mounted() {
        this.trips = await DataManagement.fetchTrips()
    }
}
</script>

<template>
    <RouterLink to="/move"><button>Retour au menu Trajets</button></RouterLink>
    <h1>Liste des trajets</h1>
    <ul>
        <li v-for="trip in trips" :key="trip.id">
            <TripVue :trip="trip"></TripVue>
        </li>
    </ul>
</template>
<style scoped>
    li:nth-child(even) {
        background-color: rgba(0, 129, 0, 0.4);
    }
    li:nth-child(odd) {
        background-color: rgba(0, 255, 0, 0.4);
    }
    li{
        list-style-type: none;
    }
</style>