<script>   
    import DataManagement from "@/classes/DataManagement.js"
    import CountryView from "@/components/CountryView.vue"
    export default{
        data(){
            return{
                countries:[]
            }
        },
        methods:{
            async updateData(){
            this.countries=await DataManagement.fetchAllCountries()
        }
        },
        async mounted() {
            this.countries=await DataManagement.fetchAllCountries()
        },
        components: {
            CountryView
        }
        

    }
</script>
<template>
    <h3>Liste des pays</h3>
    <table>
        
        <thead>  
            
            <tr id="head">
                <th></th>
                <th>Code</th>
                <th colspan="5">Nom</th>
                <th></th>
            </tr>
        </thead>
        <tbody>
           
            <tr v-for="country in countries" :key="country.paysId">
                <CountryView :country="country" @updateData="updateData"></CountryView>
            </tr>
        </tbody>
    </table>
    
</template>

<style>
    h3{
        margin-left:20%;
        width:60%;
        background-color: green;
        margin-bottom:0;
        padding-bottom:2rem;
        padding-left:0.5rem;
        padding-top:0.5rem;
    }
    table{
        margin-left:20%;
        width:60%;
        border-spacing: 0;
    }
    tbody{
        margin:0;
    }
    tr:nth-child(even) {
        background-color: rgba(0, 180, 0,0.4);
    }
    tr:nth-child(odd) {
        background-color: rgba(0, 255, 0,0.4);
    }
    #head,table{
        background-color: green;
    }
    th{
        padding-left:1rem;
        padding-right:2rem;
        text-align: left;
    }
</style>