<script>
    import ModalCountry from '@/components/ModalCountry.vue'
    export default{
        data(){
            return{
                countryToggled:false
            }
        },
        props: {
            country: Object
        },
        components:{
            ModalCountry
        },
        emits:['updateData'],
        methods:{
            img(size) {
                return "https://flagsapi.com/" + this.country.codePays +"/flat/"+size+".png"
            },
            showDetails(){
                this.countryToggled=!this.countryToggled
            },
            closeModal(){
                this.countryToggled=false
            },
            updateData(){
                this.$emit('updateData')
            }


        }
    }
</script>
<template>
    <td><img :src=this.img(32) alt=""></td>
    <td >{{country.codePays}}</td>
    <td colspan="5">{{country.nomPays}}</td>
    <td><button @click="showDetails()">Détails</button></td>
    <dialog v-if="countryToggled" open>
        <ModalCountry :country="country" @closeModal="closeModal" @updateData="updateData"></ModalCountry>
    </dialog>
</template>

<style scoped>
    dialog{
        padding:0.5rem;
        width:50%;
        text-align: center;
        background-color:#036501;
        
    }
    td{
        padding-left:1rem;
        padding-right:2rem
    }
    
</style>