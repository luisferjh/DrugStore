<template>
    <v-card>
      <v-card-title>
        <span class="headline">Editar Cliente</span>
      </v-card-title>
      <v-card-text>
        <v-form>      
          <v-container>
            <v-row>
              <v-col cols="12" sm="6" md="12">
                <v-text-field label="Nombres*" v-model="clientObj.name" required></v-text-field>
              </v-col>            
             
              <v-col cols="12" md="12">
                <v-text-field label="Apellidos*" v-model="clientObj.lastName" required></v-text-field>
              </v-col>
              
              <v-col cols="12">
                <v-text-field label="Tipo de documento" v-model="clientObj.documentType"></v-text-field>
              </v-col>                     

                <v-col cols="12">
                <v-text-field label="Numero de documento" v-model="clientObj.documentNumber" ></v-text-field>
              </v-col>

              <v-col cols="12">
                <v-text-field label="Numero de telefono" v-model="clientObj.phoneNumber" required></v-text-field>
              </v-col>
              <v-col cols="12">
                
                <v-select
                  v-model="clientObj.condition"
                  :items="[{'estado':'Activo', 'value':true}, {'estado':'Inactivo', 'value':false}]"   
                  item-text="estado"
                  item-value="value"                       
                  label="Estado*"
                  required
                ></v-select>  
              </v-col>
            </v-row>
          </v-container>
          <small>*indica campo obligatorio</small>        
        </v-form>
      </v-card-text>
      <v-card-actions>
        <div class="flex-grow-1"></div>
        <v-btn color="blue darken-1" text @click="cancel()">Cerrar</v-btn>
        <v-btn color="blue darken-1" text @click="save()">Guardar</v-btn>
      </v-card-actions>
    </v-card>
  
</template>

<script>
  export default {
    props: {     
      client:{
        type:Object,
        required:true
      }    
    },
    data() {
      return {
        clientObj: Object.assign({}, this.client)
      }
    },
    watch: {
     //este watcher nos ayuda a actualizar 
     //el producto enviado desde el componente padre
      client(newValue, oldValue) {        
       this.clientObj = Object.assign({}, newValue)
     }
   }, 
    methods: {
      save() {
        this.$emit('save',{client:this.clientObj})
      },
      cancel(){
        this.$emit('cancel',false)
      }
    },
  }
</script>
