<script>
import DataManagement from '@/classes/DataManagement';
import City from '@/classes/City';
    export default{
        data(){
            return{
                cityName:null,
                cityPostal:null
            }
        },
        props:{
            country:Object
        },
        computed:{
            cityNumber(){
                return this.country.villes.length
            }
        },
        emits:['closeModal','updateData'],
        methods:{
            closeModal(){
                this.$emit('closeModal')
            },
            updateData(){
                this.$emit('updateData')
            },
            img(size) {
                return "https://flagsapi.com/" + this.country.codePays +"/flat/"+size+".png"
            },
            async displayCities(){
                return await DataManagement.fetchCitiesByCountry(this.country.PaysId)
            },
            async postCity(){
                if (this.cityPostal==null || this.cityName==null){
                    console.log("empty field error")
                    return
                    
                    
                }
                if (this.cityPostal.length<4 || this.cityPostal.length>6 ){
                    console.log("Le code postal doit être entre 4 et 6 lettres")
                    return
                }
                if(this.cityName.length>60){
                    console.log("La base n'accepte pas de nom de plus de 60 caractères")
                    return
                }
                const city= new City(this.cityPostal,this.cityName,this.country.paysId)
                await DataManagement.postNewCity(city)
                this.updateData()
            }
        }
        
    }
</script>        


<template>    
        <button @click="closeModal()">Fermer</button>
        <div>
            <p>{{ country.nomPays }}</p>
            <img :src=this.img(64) alt="">
            <p>Code ISO: {{country.codePays}}</p>
        </div>
        <form action="" method="post">
            <div>
                <label for="addCity">Ajouter une ville: </label>
                <input type="text" id="addCity" name="addCity" placeholder="Paris" v-model="cityName">
            </div>
            <div>
                <label for="addPostal">Code postal: </label>
                <input type="text" id="addPostal" name="addPostal" placeholder="75000" v-model="cityPostal">
            </div>
            <button type="button" @click="postCity()">Ajouter</button>
        </form>
        <p>{{ cityNumber }} ville(s) enregistrées</p>
        <ul>
            <li v-for="ville in this.country.villes" :key="ville.id">
                {{ville.nomVille}}
            </li>
        </ul>

</template> 

<style scoped>
    div{
        background-color:#348332;
        color:#fcfb0e;
        display:flex;
        justify-content: space-around;
    }
    button{
        width:100%;
        background-color: rgb(182, 0, 0);
        color: white;
    }
    form>div{
        display: block;
        text-align: left;
        padding-left: 2rem;
    }
    li{
        text-align: left;
    }
</style>