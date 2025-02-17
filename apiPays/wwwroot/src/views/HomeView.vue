<script>
    import CountriesTable from "@/components/CountriesTable.vue"
    export default{
        components:{
            CountriesTable
        }
    }
</script>

<template>
    <h1>Truite Atlas</h1>
    <RouterLink to="/trip"><h2>Gestion des voyages</h2></RouterLink>
    <img id="banner" src="/banner.png" alt="">
    <h2 id="negatif">Pays</h2>
    <CountriesTable></CountriesTable>
</template>

<style scoped>
#negatif{
    margin-top: -2rem;
    margin-bottom: 2rem;
}
#banner {
        width: 100vw;
    }

    a{
      color:rgba(255, 255, 255, 0.9);
      text-decoration: none;
      &.router-link-active {
        font-weight: bold;
      }
    }
</style>