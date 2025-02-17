<script>
// import { RouterLink } from 'vue-router'
</script>
<template>
    <RouterLink to="/"><button>Accueil</button></RouterLink>
    <h1>Truite Voyages</h1>
    <p>Que voulez-vous modifier?</p>
    <ul>
        <li>
            <RouterLink to="/person">Gestion des personnes</RouterLink>
        </li>
        <li>
            <RouterLink to="/move">Gestion des trajets</RouterLink>
        </li>
    </ul>
</template>


<style scoped>
a {
    color: white;
    text-decoration: none;

    &.router-link-active {
        font-weight: bold;
    }
}
</style>