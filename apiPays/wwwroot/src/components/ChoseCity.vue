<script>
import DataManagement from '@/classes/DataManagement';
export default{
    data(){
        return{
            nomPays:""
        }
    },
    props:{
        city:Object
    },
    async mounted(){
        const countries= await DataManagement.fetchAllCountries()
        this.nomPays=countries.find((country)=>country.paysId==this.city.paysId).nomPays
    }
}
</script>

<template>
    {{ city.nomVille }}, {{ city.codePostal }} {{ nomPays }} 
</template>
