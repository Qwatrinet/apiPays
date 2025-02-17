<script>
import PersonVue from '@/components/PersonVue.vue';
import DataManagement from '@/classes/DataManagement';
export default {
    data() {
        return {
            people: []
        }
    },
    components: {
        PersonVue
    },
    async mounted() {
        this.people = await DataManagement.fetchpeople()
    }
}
</script>

<template>
    <RouterLink to="/person"><button>Retour au menu Personnes</button></RouterLink>
    <h1>Liste des personnes inscrites</h1>
    <ul>
        <li v-for="person in people" :key="person.id">
            <PersonVue :person="person"></PersonVue>
        </li>
    </ul>

</template>